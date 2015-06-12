namespace Secucard.Stomp
{
    public class StompConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string AcceptVersion { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int SocketTimeoutSec { get; set; }
        public int ReceiptTimeoutSec { get; set; }
        public int ConnectionTimeoutSec { get; set; }
        public bool DisconnectOnError { get; set; }
        public bool UseSsl { get; set; }
        public int HeartbeatClientMs { get; set; }
        public int HeartbeatServerMs { get; set; }
    }
}