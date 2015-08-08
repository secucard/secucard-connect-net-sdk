namespace Secucard.Stomp
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using Timer = System.Timers.Timer;

    public class StompClient : IDisposable
    {
        public event StompClientFrameArrivedHandler StompClientFrameArrivedEvent;
        public event StompClientStatusChangedEventHandler StompClientChangedEvent;

        public readonly ConcurrentQueue<StompFrame> InQueue; // TODO: Später wieder auflösen, um Locks zu vermeiden
        private readonly ConcurrentDictionary<string, DateTime> Receipts;
        private Timer ClientTimerHeartbeat;
        public StompConfig Config;
        private StompCore Core;
        private StompFrame Error;
        private bool IsConnected;
        public EnumStompClientStatus StompClientStatus;

        public StompClient(StompConfig config)
        {
            StompTrace.ClientTrace("StompClient Create '{0}'", config.Host);
            Config = config;
            Receipts = new ConcurrentDictionary<string, DateTime>();
            StompClientStatus = EnumStompClientStatus.NotConnected;

            InQueue = new ConcurrentQueue<StompFrame>();
        }

        public void Dispose()
        {
            if (ClientTimerHeartbeat != null) ClientTimerHeartbeat.Dispose();
            if (Core != null) Core.Dispose();
        }

        /// <summary>
        ///     sync connect
        /// </summary>
        public bool Connect()
        {
            if (Core != null) Dispose();
            Core = new StompCore(Config);
            Core.Init();
            Core.StompCoreFrameArrived += ClientOnStompCoreFrameArrived;
            OnStatusChanged(EnumStompClientStatus.Connecting);

            Core.SendFrame(CreateFrameConnect());

            // Waiting for STOMP to connect or timeout
            var waitUntil = DateTime.Now.AddSeconds(Config.HeartbeatServerMs/1000);
            while (StompClientStatus == EnumStompClientStatus.Connecting)
            {
                if (waitUntil < DateTime.Now)
                    OnStatusChanged(EnumStompClientStatus.Timeout);
            }

            if (StompClientStatus == EnumStompClientStatus.Connected)
            {
                IsConnected = true;
                //CreateClientHeartBeat();
                return true;
            }
            return false;
        }

        public void Disconnect()
        {
            // graceful shutdown
            // Frame DISCONNECT + Receipt
            IsConnected = false;
            OnStatusChanged(EnumStompClientStatus.Disconnecting);
            ClientTimerHeartbeat.Dispose();
            var frame = CreateFrameDisconnect();
            SendFrame(frame);
            OnStatusChanged(EnumStompClientStatus.Disconnected);
        }

        private void OnStatusChanged(EnumStompClientStatus status)
        {
            StompClientStatus = status;
            if(StompClientChangedEvent!=null) StompClientChangedEvent(this,new StompClientStatusChangedEventArgs{Status = status,Time = DateTime.Now});
        }

        public void SendFrame(StompFrame frame)
        {
            var rcptId = "rcpt-" + Guid.NewGuid();

            if (Config.RequestSENDReceipt) frame.Headers.Add(StompHeader.Receipt, rcptId);
            Core.SendFrame(frame);

            if (Config.RequestSENDReceipt)
                AwaitReceipt(rcptId, frame.TimeoutSec);
        }

        private void AwaitReceipt(string rcptId, int? timeoutSec)
        {
            if (timeoutSec == null)
            {
                timeoutSec = Config.ReceiptTimeoutSec;
            }

            var found = false;
            var maxWaitTime = DateTime.Now.AddSeconds(timeoutSec.Value);

            while (DateTime.Now <= maxWaitTime && IsConnected && Error == null)
            {
                DateTime rcptDateTime;
                if (Receipts.TryRemove(rcptId, out rcptDateTime))
                {
                    found = true;
                    break;
                }
                Thread.Sleep(200);
            }

            // we can treat error as reason to disconnect
            if (Error != null || !found)
            {
                if (IsConnected)
                {
                    try
                    {
                        Disconnect();
                    }
                    catch (Exception ex)
                    {
                        // ignore all
                        //LOG.error("Error disconnecting due receipt timeout or error.", t);
                    }
                }
                if (Error != null)
                {
                    var body = Error.Body;
                    var headers = Error.Headers;
                    Error = null;
                    throw new StompError(body, headers);
                }
                throw new NoReceiptException("No receipt frame received for sent message.");
            }
            // consider receipt as successful disconnect
        }

        #region ### Private ###

        private void ClientOnStompCoreFrameArrived(object sender, StompCoreFrameArrivedEventArgs args)
        {
            // analyze frame
            switch (args.Frame.Command)
            {
                case StompCommands.CONNECTED:
                {
                    // CONNECTED FRAME received set core as connected
                    OnStatusChanged(EnumStompClientStatus.Connected);
                    break;
                }
                case StompCommands.DISCONNECT:
                {
                    // CONNECTED FRAME received set core as connected
                    OnStatusChanged(EnumStompClientStatus.Disconnected);
                    break;
                }
                case StompCommands.ERROR:
                {
                    OnError(Error);
                    break;
                }
                case StompCommands.RECEIPT:
                {
                    OnReceipt(args.Frame);
                    break;
                }
                case StompCommands.MESSAGE:
                {
                    RaiseFrameArriveEventInSeparateThread(args);
                    break;
                }

                default:
                {
                    // pass frame upwards
                    RaiseFrameArriveEventInSeparateThread(args);
                    break;
                }
            }
        }

        private void RaiseFrameArriveEventInSeparateThread(StompCoreFrameArrivedEventArgs e)
        {
            // Start event in new thread, to avoid blocking core
            Task.Factory.StartNew(() => OnFrameArrived(e));
        }

        private void OnFrameArrived(StompCoreFrameArrivedEventArgs e)
        {
            lock (InQueue)
            {
                InQueue.Enqueue(e.Frame);
                StompFrame frame;
                if (InQueue.Count > 20) InQueue.TryDequeue(out frame);
            }

            if (StompClientFrameArrivedEvent != null)
            {
                StompTrace.ClientTrace("Stomp Client Frame arrived: {0}", e.Frame.GetFrame());
                StompClientFrameArrivedEvent(this, new StompClientFrameArrivedArgs {Frame = e.Frame, Time = e.Time});
            }
        }

        /// <summary>
        ///     Store receipts in local dictionary. Send is looking for receipts there.
        /// </summary>
        private void OnReceipt(StompFrame frame)
        {
            if (frame.Headers.ContainsKey(StompHeader.ReceiptId))
            {
                var receipt = frame.Headers[StompHeader.ReceiptId];
                Receipts.TryAdd(receipt, DateTime.Now);
            }
        }

        private void OnError(StompFrame frame)
        {
            Error = frame;
            OnStatusChanged(EnumStompClientStatus.Error);

        }

        private StompFrame CreateFrameConnect()
        {
            var frame = CreateFrame(StompCommands.CONNECT);
            frame.Headers.Add(StompHeader.HeartBeat,
                string.Format("{0},{1}", Config.HeartbeatClientMs, Config.HeartbeatServerMs));
            frame.Headers.Add(StompHeader.AcceptVersion, Config.AcceptVersion);
            return frame;
        }

        private StompFrame CreateFrameDisconnect()
        {
            var frame = CreateFrame(StompCommands.DISCONNECT);
            frame.Headers.Add(StompHeader.AcceptVersion, Config.AcceptVersion);

            return frame;
        }

        private StompFrame CreateFrame(string command)
        {
            var frame = new StompFrame(command);
            frame.Headers.Add(StompHeader.Login, Config.Login);
            frame.Headers.Add(StompHeader.Passcode, Config.Password);
            return frame;
        }

        #endregion
    }
}