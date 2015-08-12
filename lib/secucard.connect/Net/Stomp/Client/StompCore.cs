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
        private readonly StompConfig Config;
        private SslStream sslStream;
        private bool Stop;
        private TcpClient tcpClient;

        public StompCore(StompConfig config)
        {
            StompTrace.Info("Client Create '{0}'", config.Host);
            Config = config;
        }

        public void Dispose()
        {
            Stop = true;
            if (sslStream != null) sslStream.Dispose();
            if (tcpClient != null) tcpClient.Close();
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
                tcpClient = new TcpClient();
                tcpClient.Connect(Config.Host, Config.Port);
                StompTrace.Info("TCP connection created '{0}:{1}'", Config.Host, Config.Port);

                sslStream = new SslStream(
                    tcpClient.GetStream(),
                    false,
                    ValidateServerCertificate,
                    null
                    );

                sslStream.AuthenticateAsClient(Config.Host, null, SslProtocols.Tls12, true);
                StompTrace.Info("Client Authenticated Algo:{0} Hash:{1}, ", sslStream.CipherAlgorithm,
                    sslStream.HashAlgorithm);
            }
            catch (Exception ex)
            {
                StompTrace.Info(ex);
                throw;
            }

            Receive(sslStream);
        }

        public void SendFrame(StompFrame frame)
        {
            if (tcpClient.Connected)
            {
                var msg = frame.GetFrame();
                StompTrace.Info("SendFrame: \n{0}", msg);
                var bytes = Encoding.UTF8.GetBytes(msg + "\0"); // NULL Terminated
                sslStream.Write(bytes, 0, bytes.Length);
            }
        }

        private void Receive(SslStream stream)
        {
            try
            {
                var state = new StreamStateObject {Stream = stream};
                stream.BeginRead(state.buffer, 0, StreamStateObject.BufferSize, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                StompTrace.Info(e);
                throw;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            if (Stop) return;

            try
            {
                var state = (StreamStateObject) ar.AsyncState;
                var stream = state.Stream;

                var bytesRead = stream.EndRead(ar);

                if (bytesRead > 0)
                {
                    StompTrace.Info("Bytes read {0}", bytesRead);

                    // Add Bytes to internal list
                    state.bytes.AddRange(state.buffer.Take(bytesRead));

                    // Test if NULL exists in data received so far
                    var i = state.bytes.IndexOf(0);
                    if (i > 0)
                    {
                        // Create Frame from Bytes
                        var frame = StompFrame.CreateFrame(state.bytes.Take(i).ToArray());
                        OnFrameArrived(frame);
                        // remove used up bytes from list
                        state.bytes.RemoveRange(0, i + 1);
                    }
                }
                // Start waiting for more data

                try
                {
                    if (!Stop)
                        stream.BeginRead(state.buffer, 0, StreamStateObject.BufferSize, ReceiveCallback, state);
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