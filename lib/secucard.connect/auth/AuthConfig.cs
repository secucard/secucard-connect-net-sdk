namespace Secucard.Connect.Auth
{
    public class AuthConfig
    {
        public AuthTypeEnum AuthType { get; set; }
        public string AuthUrl { get; set; }
        public string Host { get; set; }
        public string Uuid { get; set; }
        public string PageOauthToken { get; set; }
        public string PageSmartDevices { get; set; }
        public string DeviceId { get; set; }
        public int AuthWaitTimeoutSec { get; set; }
        public ClientCredentials ClientCredentials { get; set; }
        public UserCredentials UserCredentials { get; set; }
    }
}