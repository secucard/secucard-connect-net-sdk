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
    using System.IO;
    using System.Linq;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class StompCore : IDisposable
    {
        private readonly StompConfig _config;
        private SslStream _sslStream;
        private bool _stop;
        private TcpClient _tcpClient;

        public StompCore(StompConfig config)
        {
            StompTrace.Info("Client Create '{0}'", config.Host);
            _config = config;
        }

        public void Dispose()
        {
            _stop = true;
            if (_sslStream != null) _sslStream.Dispose();
            if (_tcpClient != null) _tcpClient.Close();
        }

        public event StompCoreFrameArrivedEventHandler StompCoreFrameArrivedEvent;
        public event StompCoreExceptionEventHandler StompCoreExceptionEvent;

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public void Init()
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(_config.Host, _config.Port);
                StompTrace.Info("TCP connection created '{0}:{1}'", _config.Host, _config.Port);

                _sslStream = new SslStream(
                    _tcpClient.GetStream(),
                    false,
                    ValidateServerCertificate,
                    null
                    );

                _sslStream.AuthenticateAsClient(_config.Host, null, SslProtocols.Tls12, true);
                StompTrace.Info("Client Authenticated Algo:{0} Hash:{1}, ", _sslStream.CipherAlgorithm,
                    _sslStream.HashAlgorithm);
            }
            catch (Exception ex)
            {
                StompTrace.Info(ex);
                throw;
            }

            Receive(_sslStream);
        }

        public void SendFrame(StompFrame frame)
        {
            if (_tcpClient.Connected)
            {
                var msg = frame.GetFrame();
                StompTrace.Info("SendFrame: \n{0}", msg);
                var bytes = Encoding.UTF8.GetBytes(msg + "\0"); // NULL Terminated
                _sslStream.Write(bytes, 0, bytes.Length);
            }
        }

        private void Receive(SslStream stream)
        {
            try
            {
                var state = new StreamStateObject {Stream = stream};
                stream.BeginRead(state.Buffer, 0, StreamStateObject.BufferSize, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                StompTrace.Info(e);
                throw;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            if (_stop) return;

            try
            {
                var state = (StreamStateObject) ar.AsyncState;
                var stream = state.Stream;

                var bytesRead = stream.EndRead(ar);

                if (bytesRead > 0)
                {
                    StompTrace.Info("Bytes read {0}", bytesRead);

                    // Add Bytes to internal list
                    state.Bytes.AddRange(state.Buffer.Take(bytesRead));

                    // Test if NULL exists in data received so far
                    var i = state.Bytes.IndexOf(0);
                    if (i > 0)
                    {
                        // Create Frame from Bytes
                        var frame = StompFrame.CreateFrame(state.Bytes.Take(i).ToArray());
                        OnFrameArrived(frame);
                        // remove used up bytes from list
                        state.Bytes.RemoveRange(0, i + 1);
                    }
                }
                // Start waiting for more data

                try
                {
                    if (!_stop)
                        stream.BeginRead(state.Buffer, 0, StreamStateObject.BufferSize, ReceiveCallback, state);
                }
                catch (IOException ex)
                {
                    // Connection Close - raise event
                    OnException(ex);
                }
            }
            catch (Exception e)
            {
                StompTrace.Info(e);
                throw;
            }
        }

        private void OnFrameArrived(StompFrame frame)
        {
            StompTrace.Info("Frame Arrived Command={0}", frame.Command);
            if (StompCoreFrameArrivedEvent != null)
                StompCoreFrameArrivedEvent(this, new StompCoreFrameArrivedEventArgs {Frame = frame, Time = DateTime.Now});
        }

        private void OnException(Exception ex)
        {
            StompTrace.Info(ex);
            if (StompCoreExceptionEvent != null)
                StompCoreExceptionEvent(this, new StompCoreExceptionEventArgs {Time = DateTime.Now, Exception = ex});
        }
    }
}