namespace Secucard.Connect.Net.Stomp
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Timers;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp.Client;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Timer = System.Timers.Timer;

    public class StompChannel : Channel
    {
        private readonly string _channelId;
        private readonly StompConfig _configuration;
        private readonly object _lockSend = new object();

        private readonly ConcurrentDictionary<string, StompMessage> _messages =
            new ConcurrentDictionary<string, StompMessage>();

        private readonly StompClient _stomp;
        private Timer _clientTimerHeartbeat;
        private string _connectToken;
        private volatile bool _isConfirmed;
        private volatile bool _stopRefresh;

        public StompChannel(StompConfig configuration, ClientContext context)
            : base(context)
        {
            SecucardTrace.Info(string.Format("configuration = '{0}'", configuration));
            _configuration = configuration;

            _channelId = Guid.NewGuid().ToString();
            _stomp = new StompClient(_configuration);
            _stomp.StompClientFrameArrivedEvent += StompOnStompClientFrameArrivedEvent;
            _stomp.StompClientChangedEvent += Stomp_StompClientChangedEvent;
        }

        public StompEventArrivedEventHandler StompEventArrivedEvent;

        private void Stomp_StompClientChangedEvent(object sender, StompClientStatusChangedEventArgs args)
        {
            // TODO: Tell someone, that stomp client changed status
        }

        private void StompOnStompClientFrameArrivedEvent(object sender, StompClientFrameArrivedArgs args)
        {
            var frame = args.Frame;
            if (frame.Headers.ContainsKey(StompHeader.CorrelationId))
            {
                PutMessage(frame.Headers[StompHeader.CorrelationId], frame.Body);
            }
            else
            {
                // Event Arrived;
                OnEventArrived(frame);
            }
        }

        private void OnEventArrived(StompFrame frame)
        {
            if (StompEventArrivedEvent != null)
                StompEventArrivedEvent(this, new StompEventArrivedEventArgs {Body = frame.Body, Time = frame.CreatedAt});
        }

        public override T Request<T>(ChannelRequest request)
        {
            var stompRequest = StompRequest.Create(request, _channelId, _configuration.ReplyTo, _configuration.Destination);
            var returnMessage = SendMessage(stompRequest);
            SecucardTrace.InfoSource("StompChannel.Request", returnMessage.EscapeCurlyBracets());
            var response = new Response(returnMessage);
            return JsonSerializer.DeserializeJson<T>(response.Data);
        }

        public override ObjectList<T> RequestList<T>(ChannelRequest request)
        {
            var stompRequest = StompRequest.Create(request, _channelId, _configuration.ReplyTo, _configuration.Destination);
            var returnMessage = SendMessage(stompRequest);
            SecucardTrace.InfoSource("StompChannel.RequestList",returnMessage.EscapeCurlyBracets());
            var response = new Response(returnMessage);
            return JsonSerializer.DeserializeJson<ObjectList<T>>(response.Data);
        }

        /// <summary>
        ///     Provides the token used as login and password for STOMP connect.
        /// </summary>
        private string GetToken()
        {
            return Context.TokenManager.GetToken(false);
        }

        /// <summary>
        ///     Connect to STOMP Server.  If the connection fails all resources are closed.
        /// </summary>
        private void Connect(string token)
        {
            if (!_stomp.Connect(token, token))
            {
                throw new SecucardConnectException();
            }

            _connectToken = token;
        }

        private void CheckConnection(string token)
        {
            // auto-connect or reconnect if token has changed since last connect
            if (_stomp.StompClientStatus != EnumStompClientStatus.Connected ||
                (token != null && !token.Equals(_connectToken)))
            {
                if (_stomp.StompClientStatus == EnumStompClientStatus.Connected)
                {
                    SecucardTrace.Info("Reconnect due token change.");
                }
                try
                {
                    _stomp.Disconnect();
                }
                catch (Exception e)
                {
                    // just log...
                    SecucardTrace.Info("Error disconnecting. {0}", e);
                }
                Connect(token);
            }
        }

        private string SendMessage(StompRequest stompRequest)
        {
            var token = GetToken();

            var frame = new StompFrame(StompCommands.Send);
            frame.Headers.Add(StompHeader.ReplyTo, stompRequest.ReplayTo);
            frame.Headers.Add(StompHeader.ContentType, "application/json");
            frame.Headers.Add(StompHeader.UserId, token);
            frame.Headers.Add(StompHeader.CorrelationId, stompRequest.CorrelationId);
            frame.Headers.Add(StompHeader.Destination, stompRequest.Destination);

            if (!string.IsNullOrWhiteSpace(stompRequest.AppId))
                frame.Headers.Add(StompHeader.AppId, stompRequest.AppId);

            if (!string.IsNullOrWhiteSpace(stompRequest.Body))
            {
                frame.Body = stompRequest.Body;
                frame.Headers.Add(StompHeader.ContentLength, frame.Body.ToUTF8Bytes().Length.ToString());
            }

            // only one send at a time
            lock (_lockSend)
            {
                CheckConnection(token);
                _stomp.SendFrame(frame);
            }

            string message = null;
            var endWaitAt = DateTime.Now.AddSeconds(_configuration.MessageTimeoutSec);
            while (message == null && DateTime.Now < endWaitAt)
            {
                SecucardTrace.Info("Waiting for Message with correlationId={0}", stompRequest.CorrelationId);
                message = PullMessage(stompRequest.CorrelationId, _configuration.MaxMessageAgeSec);
                Thread.Sleep(500);
            }
            if (message == null)
            {
                throw new MessageTimeoutException("No answer for " + stompRequest.CorrelationId + " received within " +
                                                  _configuration.MessageTimeoutSec + "s.");
            }

            return message;
        }

        /// <summary>
        ///     Put message in message list
        /// </summary>
        private void PutMessage(string id, string body)
        {
            var msg = new StompMessage(id, body);
            var success = _messages.TryAdd(msg.Id, msg);
            if (!success) throw new Exception("Invalid correlation id, message with this id already exists.");
        }

        /// <summary>
        ///     Pull Message from Queue an remove old messages if exists.
        /// </summary>
        private string PullMessage(string id, int maxMessageAgeSec)
        {
            var t = DateTime.Now.AddSeconds(-maxMessageAgeSec);
            StompMessage message;

            var toOld = _messages.Where(o => o.Value.ReceiveTime < t).ToList();
            foreach (var m in toOld)
            {
                _messages.TryRemove(m.Key, out message);
            }

            if (_messages.TryRemove(id, out message))
            {
                return message.Body ?? "";
            }
            return null;
        }

        #region ### Open / Close ###

        public override void Open()
        {
            // TOOD: Connect an start listening
            Connect(GetToken());
            SecucardTrace.Info("STOMP channel opened.");
            StartSessionRefresh();
        }

        public override void Close()
        {
            _stopRefresh = true;
            _clientTimerHeartbeat.Dispose();
            _stomp.Disconnect();
            SecucardTrace.Info("STOMP channel closed.");
        }

        #endregion

        #region ### Stomp Refresh Heartbeat ###

        private bool? SendSessionRefresh()
        {
            var channelRequest = new ChannelRequest
            {
                Method = ChannelMethod.Execute,
                Product = "auth",
                Resource = "sessions",
                Action = "refresh",
                ObjectId = "me"
            };

            var stompRequest = StompRequest.Create(channelRequest, _channelId, _configuration.ReplyTo,
                _configuration.Destination);
            var returnMessage = SendMessage(stompRequest);

            var response = new Response(returnMessage);
            return JsonSerializer.DeserializeJson<StompResult>(response.Data).Result;
        }

        private void StartSessionRefresh()
        {
            _clientTimerHeartbeat = new Timer(_configuration.HeartbeatMs) {AutoReset = true};
            _clientTimerHeartbeat.Elapsed += ClientTimerOnElapsed;
            _clientTimerHeartbeat.Start();
        }

        private void ClientTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (_stopRefresh) return;

            if (_isConfirmed)
            {
                // There has been a message on Stomp within the cycle. No need to send refresh
                _isConfirmed = false;
            }
            else
            {
                // Send new session refresh message to keep stomp connection alive.
                _isConfirmed = false;
                var sessionOK = SendSessionRefresh();
                // TODO: if not ok ?
            }
        }

        #endregion
    }
}