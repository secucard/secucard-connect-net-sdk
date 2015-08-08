namespace Secucard.Stomp
{
    public class StompConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Destination { get; set; }
        public string ReplyTo { get; set; }
        public bool Ssl { get; set; }

        public string AcceptVersion { get; set; }
        public string Login { get; set; } // TODO: move 
        public string Password { get; set; }
        public int SocketTimeoutSec { get; set; }
        public int ReceiptTimeoutSec { get; set; }
        public int ConnectionTimeoutSec { get; set; }
        public int MessageTimeoutSec { get; set; }
        public int MaxMessageAgeSec { get; set; }
        public bool DisconnectOnError { get; set; }
        public int HeartbeatClientMs { get; set; }
        public int HeartbeatServerMs { get; set; }
        public bool RequestDISCONNECTReceipt { get; set; }
        public int DisconnectOnSENDReceiptTimeout { get; set; }
        public bool RequestSENDReceipt { get; set; }
    }
}