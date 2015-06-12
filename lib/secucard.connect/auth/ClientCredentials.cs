namespace Secucard.Connect.Auth
{
    public sealed class ClientCredentials
    {
        public ClientCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public override string ToString()
        {
            return string.Format("OAuthClientCredentials{{clientId='{0}', clientSecret='{1}'}}", ClientId, ClientSecret);
        }
    }
}