namespace Secucard.Stomp
{
    using System;
    using System.Linq;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class StompCore : IDisposable
    {
        public event StompCoreFrameArrivedEventHandler StompCoreFrameArrived;

        private readonly StompConfig Config;
        private SslStream sslStream;
        private bool Stop;
        private TcpClient tcpClient;

        public StompCore(StompConfig config)
        {
            StompTrace.ClientTrace("Client Create '{0}'", config.Host);
            Config = config;
        }

        public void Dispose()
        {
            Stop = true;
            if (sslStream != null) sslStream.Dispose();
            if (tcpClient != null) tcpClient.Close();
        }


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
                StompTrace.ClientTrace("TCP connection created '{0}:{1}'", Config.Host, Config.Port);

                sslStream = new SslStream(
                    tcpClient.GetStream(),
                    false,
                    ValidateServerCertificate,
                    null
                    );

                sslStream.AuthenticateAsClient(Config.Host, null, SslProtocols.Tls12, true);
                StompTrace.ClientTrace("Client Authenticated Algo:{0} Hash:{1}, ", sslStream.CipherAlgorithm,
                    sslStream.HashAlgorithm);
            }
            catch (Exception ex)
            {
                StompTrace.ClientTrace(ex);
                throw;
            }

            Receive(sslStream);
        }

        public void SendFrame(StompFrame frame)
        {
            if (tcpClient.Connected)
            {
                var msg = frame.GetFrame();
                StompTrace.ClientTrace("SendFrame: {0}", msg);
                var bytes = Encoding.UTF8.GetBytes(msg + "\0"); // NULL Terminated
                sslStream.Write(bytes, 0, bytes.Length);
            }
        }

        //public void SendHeartBeat()
        //{
        //    if (tcpClient.Connected)
        //    {
        //        StompTrace.ClientTrace("SendHeartBeat: {0}", DateTime.Now);
        //        var bytes = Encoding.UTF8.GetBytes("\n\0"); // NULL Terminated
        //        sslStream.Write(bytes, 0, bytes.Length);
        //    }
        //}

        private void Receive(SslStream stream)
        {
            try
            {
                var state = new StreamStateObject {Stream = stream};
                stream.BeginRead(state.buffer, 0, StreamStateObject.BufferSize, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                StompTrace.ClientTrace(e);
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
                    StompTrace.ClientTrace("Bytes read {0}", bytesRead);

                    // Add Bytes to internal list
                    state.bytes.AddRange(state.buffer.Take(bytesRead));

                    // Test if NULL exists in data received so far
                    var i = state.bytes.IndexOf(0);
                    if (i > 0)
                    {
                        // Create Frame from Bytes
                        var frame = StompFrame.CreateFrame(state.bytes.Take(i).ToArray());
                        OnFrameArrived(new StompCoreFrameArrivedEventArgs {Frame = frame, Time = DateTime.Now});
                        // remove used up bytes from list
                        state.bytes.RemoveRange(0, i + 1);
                    }
                    else
                    {
                        // Cleanup Heartbeat
                        state.bytes.RemoveRange(0, 1);
                    }
                }

                // Start waiting for more data
                if (!Stop) stream.BeginRead(state.buffer, 0, StreamStateObject.BufferSize, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                StompTrace.ClientTrace(e);
                throw;
            }
        }

        private void OnFrameArrived(StompCoreFrameArrivedEventArgs e)
        {
            if (StompCoreFrameArrived != null)
                StompCoreFrameArrived(this, e);
        }
    }
}