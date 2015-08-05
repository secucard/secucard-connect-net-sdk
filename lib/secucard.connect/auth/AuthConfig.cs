namespace Secucard.Connect.Auth
{
    public class AuthConfig
    {
        public string OAuthUrl { get; set; }
        public AuthTypeEnum AuthType { get; set; }
        public string Host { get; set; }
        public string Uuid { get; set; }
        public string DeviceId { get; set; }
        public int WaitTimeoutSec { get; set; }
        //public ClientCredentials ClientCredentials { get; set; }
        //public UserCredentials UserCredentials { get; set; }
    }
}