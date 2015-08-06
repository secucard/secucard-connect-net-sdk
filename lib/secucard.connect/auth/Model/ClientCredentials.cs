namespace Secucard.Connect.auth.Model
{
    using System.Collections.Generic;

    public class ClientCredentials : OAuthCredentials
    {
        public ClientCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public override string GrantType
        {
            get { return "client_credentials"; }
        }

        public override string Id
        {
            get { return GrantType + ClientId + ClientSecret; }
        }

        public override Dictionary<string, object> AsMap()
        {
            var map = base.AsMap();
            map.Add("client_id", ClientId);
            map.Add("client_secret", ClientSecret);
            return map;
        }
    }
}