/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
        private readonly string ChannelId;
        private readonly StompConfig Configuration;
        private readonly object lockSend = new object();

        private readonly ConcurrentDictionary<string, StompMessage> Messages =
            new ConcurrentDictionary<string, StompMessage>();

        private readonly StompClient Stomp;
        private Timer ClientTimerHeartbeat;
        private string ConnectToken;
        private volatile bool IsConfirmed;
        private volatile bool StopRefresh;

        public StompChannel(StompConfig configuration, ClientContext context)
            : base(context)
        {
            SecucardTrace.Info(string.Format("configuration = '{0}'", configuration));
            Configuration = configuration;

            ChannelId = Guid.NewGuid().ToString();
            Stomp = new StompClient(Configuration);
            Stomp.StompClientFrameArrivedEvent += StompOnStompClientFrameArrivedEvent;
            Stomp.StompClientChangedEvent += Stomp_StompClientChangedEvent;
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
            var stompRequest = StompRequest.Create(request, ChannelId, Configuration.ReplyTo, Configuration.Destination);
            var returnMessage = SendMessage(stompRequest);
            SecucardTrace.InfoSource("StompChannel.Request", returnMessage.EscapeCurlyBracets());
            var response = new Response(returnMessage);
            return JsonSerializer.DeserializeJson<T>(response.Data);
        }

        public override ObjectList<T> RequestList<T>(ChannelRequest request)
        {
            var stompRequest = StompRequest.Create(request, ChannelId, Configuration.ReplyTo, Configuration.Destination);
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
            Stomp.Connect(token,token);
            ConnectToken = token;
        }

        private void CheckConnection(string token)
        {
            // auto-connect or reconnect if token has changed since last connect
            if (Stomp.StompClientStatus != EnumStompClientStatus.Connected ||
                (token != null && !token.Equals(ConnectToken)))
            {
                if (Stomp.StompClientStatus == EnumStompClientStatus.Connected)
                {
                    SecucardTrace.Info("Reconnect due token change.");
                }
                try
                {
                    Stomp.Disconnect();
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

            var frame = new StompFrame(StompCommands.SEND);
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
            lock (lockSend)
            {
                CheckConnection(token);
                Stomp.SendFrame(frame);
            }

            string message = null;
            var endWaitAt = DateTime.Now.AddSeconds(Configuration.MessageTimeoutSec);
            while (message == null && DateTime.Now < endWaitAt)
            {
                SecucardTrace.Info("Waiting for Message with correlationId={0}", stompRequest.CorrelationId);
                message = PullMessage(stompRequest.CorrelationId, Configuration.MaxMessageAgeSec);
                Thread.Sleep(500);
            }
            if (message == null)
            {
                throw new MessageTimeoutException("No answer for " + stompRequest.CorrelationId + " received within " +
                                                  Configuration.MessageTimeoutSec + "s.");
            }

            return message;
        }

        /// <summary>
        ///     Put message in message list
        /// </summary>
        private void PutMessage(string id, string body)
        {
            var msg = new StompMessage(id, body);
            var success = Messages.TryAdd(msg.Id, msg);
            if (!success) throw new Exception("Invalid correlation id, message with this id already exists.");
        }

        /// <summary>
        ///     Pull Message from Queue an remove old messages if exists.
        /// </summary>
        private string PullMessage(string id, int maxMessageAgeSec)
        {
            var t = DateTime.Now.AddSeconds(-maxMessageAgeSec);
            StompMessage message;

            var toOld = Messages.Where(o => o.Value.ReceiveTime < t).ToList();
            foreach (var m in toOld)
            {
                Messages.TryRemove(m.Key, out message);
            }

            if (Messages.TryRemove(id, out message))
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
            StopRefresh = true;
            ClientTimerHeartbeat.Dispose();
            Stomp.Disconnect();
            SecucardTrace.Info("STOMP channel closed.");
        }

        #endregion

        #region ### Stomp Refresh Heartbeat ###

        private bool? SendSessionRefresh()
        {
            var channelRequest = new ChannelRequest
            {
                Method = ChannelMethod.EXECUTE,
                Product = "auth",
                Resource = "sessions",
                Action = "refresh",
                ObjectId = "me"
            };

            var stompRequest = StompRequest.Create(channelRequest, ChannelId, Configuration.ReplyTo,
                Configuration.Destination);
            var returnMessage = SendMessage(stompRequest);

            var response = new Response(returnMessage);
            return JsonSerializer.DeserializeJson<StompResult>(response.Data).Result;
        }

        private void StartSessionRefresh()
        {
            ClientTimerHeartbeat = new Timer(Configuration.HeartbeatMs) {AutoReset = true};
            ClientTimerHeartbeat.Elapsed += ClientTimerOnElapsed;
            ClientTimerHeartbeat.Start();
        }

        private void ClientTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (StopRefresh) return;

            if (IsConfirmed)
            {
                // There has been a message on Stomp within the cycle. No need to send refresh
                IsConfirmed = false;
            }
            else
            {
                // Send new session refresh message to keep stomp connection alive.
                IsConfirmed = false;
                var sessionOK = SendSessionRefresh();
                // TODO: if not ok ?
            }
        }

        #endregion
    }
}