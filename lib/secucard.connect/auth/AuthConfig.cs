namespace Secucard.Connect.Auth
{
    public class AuthConfig
    {
        public string OAuthUrl { get; set; }
        public int AuthWaitTimeoutSec { get; set; }
        public bool ExtendExpire { get; set; }

        public AuthTypeEnum AuthType { get; set; }
        public string Host { get; set; }
        public string Uuid { get; set; }

    }
}