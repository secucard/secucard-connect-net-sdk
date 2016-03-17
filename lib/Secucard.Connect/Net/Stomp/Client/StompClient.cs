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

namespace Secucard.Connect.Net.Stomp.Client
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Secucard.Connect.Client;

    public class StompClient : IDisposable
    {
        private readonly ConcurrentQueue<StompFrame> InQueue;
        private readonly ConcurrentDictionary<string, DateTime> Receipts;
        private readonly StompConfig Config;
        private StompCore Core;
        private StompFrame Error;
        private bool IsConnected;
        public EnumStompClientStatus StompClientStatus;
        private string Login;
        private string Password;

        public StompClient(StompConfig config)
        {
            StompTrace.Info("StompClient Create '{0}'", config.Host);
            Config = config;
            Receipts = new ConcurrentDictionary<string, DateTime>();
            StompClientStatus = EnumStompClientStatus.NotConnected;

            InQueue = new ConcurrentQueue<StompFrame>();
        }

        public void Dispose()
        {
            if (Core != null) Core.Dispose();
        }

        public event StompClientFrameArrivedHandler StompClientFrameArrivedEvent;
        public event StompClientStatusChangedEventHandler StompClientChangedEvent;

        /// <summary>
        ///     sync connect
        /// </summary>
        public bool Connect(string login, string password)
        {
            Login = login;
            Password = password;

            if (Core != null) Dispose();
            Core = new StompCore(Config);
            Core.Init();
            Core.StompCoreFrameArrivedEvent += ClientOnStompCoreFrameArrived;
            Core.StompCoreExceptionEvent += Core_StompCoreExceptionEvent;
            OnStatusChanged(EnumStompClientStatus.Connecting);

            Core.SendFrame(CreateFrameConnect());

            // Waiting for STOMP to connect or timeout
            var waitUntil = DateTime.Now.AddSeconds(Config.ConnectionTimeoutSec);
            while (StompClientStatus == EnumStompClientStatus.Connecting)
            {
                if (waitUntil < DateTime.Now)
                { 
                    OnStatusChanged(EnumStompClientStatus.Timeout);
                    break;
                }
            }

            if (StompClientStatus == EnumStompClientStatus.Connected)
            {
                IsConnected = true;
                return true;
            }
            return false;
        }

        private void Core_StompCoreExceptionEvent(object sender, StompCoreExceptionEventArgs args)
        {
            OnStatusChanged(EnumStompClientStatus.NotConnected);
            Connect(Login, Password);
        }

        public void Disconnect()
        {
            IsConnected = false;
            OnStatusChanged(EnumStompClientStatus.Disconnecting);
            var frame = CreateFrameDisconnect();
            SendFrame(frame);
            OnStatusChanged(EnumStompClientStatus.Disconnected);
        }

        private void OnStatusChanged(EnumStompClientStatus status)
        {
            StompClientStatus = status;
            if (StompClientChangedEvent != null)
                StompClientChangedEvent(this,
                    new StompClientStatusChangedEventArgs {Status = status, Time = DateTime.Now});
        }

        public void SendFrame(StompFrame frame)
        {
            var rcptId = "rcpt-" + Guid.NewGuid();

            if (Config.RequestSENDReceipt && frame.Command != StompCommands.DISCONNECT)
                frame.Headers.Add(StompHeader.Receipt, rcptId);
            Core.SendFrame(frame);

            if (Config.RequestSENDReceipt && frame.Command != StompCommands.DISCONNECT)
                AwaitReceipt(rcptId, frame.TimeoutSec);
        }

        private void AwaitReceipt(string rcptId, int? timeoutSec)
        {
            if (timeoutSec == null)
            {
                timeoutSec = Config.MessageTimeoutSec;
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
                        SecucardTrace.Error("StompClient.AwaitReceipt", ex.Message);
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
            // Remove messages from inQueue if there are more than 20
            InQueue.Enqueue(e.Frame);
            StompFrame frame;
            while (InQueue.Count > 20) InQueue.TryDequeue(out frame);

            if (StompClientFrameArrivedEvent != null)
            {
                StompTrace.Info("Stomp Client Frame arrived: \n{0}", e.Frame.GetFrame());
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

            // tell server about requested heartbeat
            frame.Headers.Add(StompHeader.HeartBeat, string.Format("{0},{1}", Config.HeartbeatMs, Config.HeartbeatMs));

            frame.Headers.Add(StompHeader.AcceptVersion, Config.AcceptVersion);

            // Add virtual host if requested
            if (!string.IsNullOrWhiteSpace(Config.VirtualHost))
                frame.Headers.Add(StompHeader.Host, Config.VirtualHost);

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
            frame.Headers.Add(StompHeader.Login, Login);
            frame.Headers.Add(StompHeader.Passcode, Password);
            return frame;
        }

        #endregion
    }
}