namespace secucard.stomp
{
    public static class StompCommands
    {
        // SERVER

        public const string CONNECTED = "CONNECTED";
        public const string ERROR = "ERROR";
        public const string MESSAGE = "MESSAGE";
        public const string RECEIPT = "RECEIPT";
        // CLIENT
        public const string SEND = "SEND";
        public const string SUBSCRIBE = "SUBSCRIBE";
        public const string UNSUBSCRIBE = "UNSUBSCRIBE";
        public const string BEGIN = "BEGIN";
        public const string COMMIT = "COMMIT";
        public const string ABORT = "ABORT";
        public const string ACK = "ACK";
        public const string NACK = "NACK";
        public const string DISCONNECT = "DISCONNECT";
        public const string CONNECT = "CONNECT";
        public const string STOMP = "STOMP";
    }
}