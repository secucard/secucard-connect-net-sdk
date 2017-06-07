namespace Secucard.Connect.Net.Stomp.Client
{
    public static class StompCommands
    {
        // SERVER

        public const string Connected = "CONNECTED";
        public const string Error = "ERROR";
        public const string Message = "MESSAGE";
        public const string Receipt = "RECEIPT";
        // CLIENT
        public const string Send = "SEND";
        public const string Subscribe = "SUBSCRIBE";
        public const string Unsubscribe = "UNSUBSCRIBE";
        public const string Begin = "BEGIN";
        public const string Commit = "COMMIT";
        public const string Abort = "ABORT";
        public const string Ack = "ACK";
        public const string Nack = "NACK";
        public const string Disconnect = "DISCONNECT";
        public const string Connect = "CONNECT";
        public const string Stomp = "STOMP";
    }
}