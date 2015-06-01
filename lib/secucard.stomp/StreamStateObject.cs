namespace secucard.stomp
{
    using System.Collections.Generic;
    using System.Net.Security;

    public class StreamStateObject
    {
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data so far
        public List<byte> bytes = new List<byte>();
        // Client socket.
        public SslStream Stream = null;
    }
}