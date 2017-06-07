namespace Secucard.Connect.Auth.Model
{
    public class DeviceCredentials : ClientCredentials
    {
        public DeviceCredentials(string clientId, string clientSecret, string deviceId) :
            base(clientId, clientSecret)
        {
            DeviceId = deviceId;
        }

        /// <summary>
        /// Code obtained during the authorization process
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// A unique device id like UUID.
        /// </summary>
        public string DeviceId { get; set; }

        public override string Id
        {
            get { return "Device-" + ClientId + ClientSecret + DeviceId; }
        }
    }
}