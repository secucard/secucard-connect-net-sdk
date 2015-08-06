namespace Secucard.Stomp
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Timers;

    public delegate void StompClientFrameArrived(object sender, StompClientFrameArrivedEventArgs args);

    public class StompClient : IDisposable
    {
        public StompConfig Config;
        private Timer ClientTimerHeartbeat;
        private StompCore Core;
        public readonly ConcurrentQueue<StompFrame> InQueue; // TODO: Später wieder auflösen, um Locks zu vermeiden
        private DateTime LastServerFrame; // TODO: Heartbeat Server überwachen
        public EnumStompCoreStatus StompClientStatus;

        public StompClient(StompConfig config)
        {
            StompTrace.ClientTrace("StompClient Create '{0}'", config.Host);
            Config = config;
            StompClientStatus = EnumStompCoreStatus.NotConnected;

            InQueue = new ConcurrentQueue<StompFrame>();
            LastServerFrame = DateTime.Now;
        }

        public void Dispose()
        {
            if (ClientTimerHeartbeat != null) ClientTimerHeartbeat.Dispose();
            if (Core != null) Core.Dispose();
        }

        public event StompClientFrameArrived StompClientFrameArrived;

        /// <summary>
        ///     sync connect
        /// </summary>
        public bool Connect()
        {
            if (Core != null) Dispose();
            Core = new StompCore(Config);
            Core.Init();
            Core.StompCoreFrameArrived += ClientOnStompCoreFrameArrived;
            StompClientStatus = EnumStompCoreStatus.Connecting;

            Core.SendFrame(CreateFrameConnect());

            // Waiting for STOMP to connect or timeout
            var waitUntil = DateTime.Now.AddSeconds(Config.HeartbeatServerMs/1000);
            while (StompClientStatus == EnumStompCoreStatus.Connecting)
            {
                if (waitUntil < DateTime.Now) StompClientStatus = EnumStompCoreStatus.Timeout;
            }

            if (StompClientStatus == EnumStompCoreStatus.Connected)
            {
                CreateClientHeartBeat();
                return true;
            }
            return false;
        }

        public void Disconnect()
        {
            // graceful shutdown
            // Frame DISCONNECT + Receipt
            StompClientStatus = EnumStompCoreStatus.Disconnecting;
            ClientTimerHeartbeat.Dispose();
            var frame = CreateFrameDisconnect();
            frame.Headers.Add(StompHeader.Receipt, Guid.NewGuid().ToString());
            SendFrame(frame);
        }

        public void SendFrame(StompFrame frame)
        {
            Core.SendFrame(frame);
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
                    StompClientStatus = EnumStompCoreStatus.Connected;
                    break;
                }
                case StompCommands.ERROR:
                {
                    // CONNECTED FRAME received set core as connected
                    if (StompClientStatus == EnumStompCoreStatus.Connecting)
                        StompClientStatus = EnumStompCoreStatus.Error;
                    else
                    {
                        // pass frame upwards
                        RaiseFrameArriveEventInSeparateThread(args);
                    }
                    break;
                }
                case StompCommands.RECEIPT:
                {
                    if (StompClientStatus == EnumStompCoreStatus.Disconnecting)
                    {
                        // TODO: analyze Receipt
                        StompClientStatus = EnumStompCoreStatus.Disconnected;
                        break;
                    }

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

            if (StompClientFrameArrived != null)
            {
                StompTrace.ClientTrace("Stomp Client Frame arrived: {0}", e.Frame.GetFrame());
                StompClientFrameArrived(this, new StompClientFrameArrivedEventArgs {Frame = e.Frame, Time = e.Time});
            }
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

        private void CreateClientHeartBeat()
        {
            ClientTimerHeartbeat = new Timer(Config.HeartbeatClientMs) {AutoReset = true};
            ClientTimerHeartbeat.Elapsed += ClientTimerOnElapsed;
            ClientTimerHeartbeat.Start();
        }

        private void ClientTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Core.SendHeartBeat();
        }

        #endregion
    }
}