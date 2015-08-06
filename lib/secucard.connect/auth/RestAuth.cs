namespace Secucard.Connect.auth
{
    using System.Collections.Specialized;
    using Secucard.Connect.auth.Model;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;

    public class RestAuth : RestBase
    {
        private readonly AuthConfig AuthConfig;

        public RestAuth(AuthConfig authConfig)
            : base(new RestConfig {BaseUrl = authConfig.OAuthUrl})
        {
            AuthConfig = authConfig;
        }

        public string UserAgentInfo { get; set; }

        public DeviceAuthCode GetDeviceAuthCode(string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);
            req.BodyParameter.Add(AuthConst.Uuid, AuthConfig.Uuid);

            var deviceAuthCode = RestPost<DeviceAuthCode>(req);
            return deviceAuthCode;
        }

        public Token GetToken(string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.ClientCredentials);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);

            var userAuthtoken = RestPost<Token>(req);
            return userAuthtoken;
        }

        public Token ObtainAuthToken(string deviceCode, string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);
            req.BodyParameter.Add(AuthConst.Code, deviceCode);

            try
            {
                return RestPost<Token>(req);
            }
            catch (RestException ex)
            {
                // ignore 401 Error and return null
                if (ex.StatusCode == 401) return null;
                throw;
            }
        }

        public Token RefreshToken(string refreshToken, string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };
            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.RrefreshToken);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);
            req.BodyParameter.Add(AuthConst.RefreshToken, refreshToken);

            var token = RestPost<Token>(req);
            return token;
        }
    }
}