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
        private readonly ConcurrentQueue<StompFrame> _inQueue;
        private readonly ConcurrentDictionary<string, DateTime> _receipts;
        private readonly StompConfig _config;
        private StompCore _core;
        private StompFrame _error;
        private bool _isConnected;
        public EnumStompClientStatus StompClientStatus;
        private string _login;
        private string _password;

        public StompClient(StompConfig config)
        {
            StompTrace.Info("StompClient Create '{0}'", config.Host);
            _config = config;
            _receipts = new ConcurrentDictionary<string, DateTime>();
            StompClientStatus = EnumStompClientStatus.NotConnected;

            _inQueue = new ConcurrentQueue<StompFrame>();
        }

        public void Dispose()
        {
            if (_core != null) _core.Dispose();
        }

        public event StompClientFrameArrivedHandler StompClientFrameArrivedEvent;
        public event StompClientStatusChangedEventHandler StompClientChangedEvent;

        /// <summary>
        /// Connect to STOMP [sync]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>bool - Returns whether the connection could be established successfully</returns>
        public bool Connect(string login, string password)
        {
            _login = login;
            _password = password;

            if (_core != null) Dispose();
            _core = new StompCore(_config);
            _core.Init();
            _core.StompCoreFrameArrivedEvent += ClientOnStompCoreFrameArrived;
            _core.StompCoreExceptionEvent += Core_StompCoreExceptionEvent;
            OnStatusChanged(EnumStompClientStatus.Connecting);

            _core.SendFrame(CreateFrameConnect());

            // Waiting for STOMP to connect or timeout
            var waitUntil = DateTime.Now.AddSeconds(_config.ConnectionTimeoutSec);
            while (StompClientStatus == EnumStompClientStatus.Connecting)
            {
                if (waitUntil < DateTime.Now)
                { 
                    OnStatusChanged(EnumStompClientStatus.Timeout);
                    _isConnected = false;
                    break;
                }
            }

            if (StompClientStatus == EnumStompClientStatus.Connected)
            {
                _isConnected = true;
            }
            else if (StompClientStatus == EnumStompClientStatus.Error)
            {
                OnStatusChanged(EnumStompClientStatus.Error);
                _isConnected = false;
            }

            return _isConnected;
        }

        private void Core_StompCoreExceptionEvent(object sender, StompCoreExceptionEventArgs args)
        {
            OnStatusChanged(EnumStompClientStatus.NotConnected);
            Connect(_login, _password);
        }

        /// <summary>
        /// Disconnect from STOMP
        /// </summary>
        public void Disconnect()
        {
            _isConnected = false;
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

        /// <summary>
        /// Sends the STOMP frame
        /// </summary>
        /// <param name="frame">STOMP frame to send</param>
        public void SendFrame(StompFrame frame)
        {
            var rcptId = "rcpt-" + Guid.NewGuid();

            if (_config.RequestSENDReceipt && frame.Command != StompCommands.Disconnect)
                frame.Headers.Add(StompHeader.Receipt, rcptId);
            _core.SendFrame(frame);

            if (_config.RequestSENDReceipt && frame.Command != StompCommands.Disconnect)
                AwaitReceipt(rcptId, frame.TimeoutSec);
        }

        private void AwaitReceipt(string rcptId, int? timeoutSec)
        {
            if (timeoutSec == null)
            {
                timeoutSec = _config.MessageTimeoutSec;
            }

            var found = false;
            var maxWaitTime = DateTime.Now.AddSeconds(timeoutSec.Value);

            while (DateTime.Now <= maxWaitTime && _isConnected && _error == null)
            {
                DateTime rcptDateTime;
                if (_receipts.TryRemove(rcptId, out rcptDateTime))
                {
                    found = true;
                    break;
                }
                Thread.Sleep(200);
            }

            // we can treat error as reason to disconnect
            if (_error != null || !found)
            {
                if (_isConnected)
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
                if (_error != null)
                {
                    var body = _error.Body;
                    var headers = _error.Headers;
                    _error = null;
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
                case StompCommands.Connected:
                {
                    // CONNECTED FRAME received set core as connected
                    OnStatusChanged(EnumStompClientStatus.Connected);
                    break;
                }
                case StompCommands.Disconnect:
                {
                    // CONNECTED FRAME received set core as connected
                    OnStatusChanged(EnumStompClientStatus.Disconnected);
                    break;
                }
                case StompCommands.Error:
                {
                    OnError(_error);
                    break;
                }
                case StompCommands.Receipt:
                {
                    OnReceipt(args.Frame);
                    break;
                }
                case StompCommands.Message:
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
            _inQueue.Enqueue(e.Frame);
            StompFrame frame;
            while (_inQueue.Count > 20) _inQueue.TryDequeue(out frame);

            if (StompClientFrameArrivedEvent != null)
            {
                StompTrace.Info("Stomp Client Frame arrived: \n{0}", e.Frame.GetFrame());
                StompClientFrameArrivedEvent(this, new StompClientFrameArrivedArgs {Frame = e.Frame, Time = e.Time});
            }
        }

        /// <summary>
        /// Store receipts in local dictionary. Send is looking for receipts there.
        /// </summary>
        /// <param name="frame"></param>
        private void OnReceipt(StompFrame frame)
        {
            if (frame.Headers.ContainsKey(StompHeader.ReceiptId))
            {
                var receipt = frame.Headers[StompHeader.ReceiptId];
                _receipts.TryAdd(receipt, DateTime.Now);
            }
        }

        private void OnError(StompFrame frame)
        {
            _error = frame;
            OnStatusChanged(EnumStompClientStatus.Error);
        }

        private StompFrame CreateFrameConnect()
        {
            var frame = CreateFrame(StompCommands.Connect);

            // tell server about requested heartbeat
            frame.Headers.Add(StompHeader.HeartBeat, string.Format("{0},{1}", _config.HeartbeatMs, _config.HeartbeatMs));

            frame.Headers.Add(StompHeader.AcceptVersion, _config.AcceptVersion);

            // Add virtual host if requested
            if (!string.IsNullOrWhiteSpace(_config.VirtualHost))
                frame.Headers.Add(StompHeader.Host, _config.VirtualHost);

            return frame;
        }

        private StompFrame CreateFrameDisconnect()
        {
            var frame = CreateFrame(StompCommands.Disconnect);
            frame.Headers.Add(StompHeader.AcceptVersion, _config.AcceptVersion);

            return frame;
        }

        private StompFrame CreateFrame(string command)
        {
            var frame = new StompFrame(command);
            frame.Headers.Add(StompHeader.Login, _login);
            frame.Headers.Add(StompHeader.Passcode, _password);
            return frame;
        }

        #endregion
    }
}