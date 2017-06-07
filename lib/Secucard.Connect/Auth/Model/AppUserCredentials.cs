namespace Secucard.Connect.Auth.Model
{
    public class AppUserCredentials : ClientCredentials
    {
        public AppUserCredentials(string clientId, string clientSecret, string userName, string password,
            string deviceId) :
            base(clientId, clientSecret)
        {
            UserName = userName;
            Password = password;
            DeviceId = deviceId;
        }

        public AppUserCredentials(ClientCredentials clientCredentials, string userName, string password, string deviceId)
            :
                this(clientCredentials.ClientId, clientCredentials.ClientSecret, userName, password, deviceId)
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }


        /// <summary>
        /// A unique device id like UUID. May be optional for some credential types.
        /// </summary>
        public string DeviceId { get; set; }
        
        public override string Id
        {
            get { return "appuser-" + ClientId + ClientSecret + UserName + Password + (DeviceId ?? ""); }
        }
    }
}