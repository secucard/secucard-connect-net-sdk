namespace Secucard.Connect.Auth.Model
{
    public class ClientCredentials : OAuthCredentials
    {
        public ClientCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public override string Id
        {
            get { return "Client-" + ClientId + ClientSecret; }
        }
    }
}