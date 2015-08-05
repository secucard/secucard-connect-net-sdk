namespace Secucard.Connect.auth
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Model.Auth;
    using AuthToken = Secucard.Connect.auth.Model.Token;
    using DeviceAuthCode = Secucard.Connect.auth.Model.DeviceAuthCode;

    public class RestAuth : RestBase
    {
        private readonly AuthConfig AuthConfig;
        public string UserAgentInfo { get; set; }

        public RestAuth(AuthConfig authConfig)
            : base(new RestConfig { BaseUrl = authConfig.OAuthUrl })
        {
            AuthConfig = authConfig;
        }

        public DeviceAuthCode GetDeviceAuthCode()
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            req.BodyParameter.Add(AuthConst.Client_Id, AuthConfig.ClientCredentials.ClientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, AuthConfig.ClientCredentials.ClientSecret);
            req.BodyParameter.Add(AuthConst.Uuid, AuthConfig.Uuid);

            var deviceAuthCode = RestPost<DeviceAuthCode>(req);
            return deviceAuthCode;
        }

        public AuthToken GetToken()
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.ClientCredentials);
            req.BodyParameter.Add(AuthConst.Client_Id, AuthConfig.ClientCredentials.ClientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, AuthConfig.ClientCredentials.ClientSecret);

            var userAuthtoken = RestPost<AuthToken>(req);
            return userAuthtoken;
        }

        public AuthToken ObtainAuthToken(string deviceCode)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            req.BodyParameter.Add(AuthConst.Client_Id, AuthConfig.ClientCredentials.ClientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, AuthConfig.ClientCredentials.ClientSecret);
            req.BodyParameter.Add(AuthConst.Code, deviceCode);

            try
            {
                return RestPost<AuthToken>(req);
            }
            catch (RestException ex)
            {
                // ignore 401 Error and return null
                if (ex.StatusCode == 401) return null;
                throw;
            }
        }

        public AuthToken RefreshToken(string refreshToken)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.RrefreshToken);
            req.BodyParameter.Add(AuthConst.Client_Id, AuthConfig.ClientCredentials.ClientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, AuthConfig.ClientCredentials.ClientSecret);
            req.BodyParameter.Add(AuthConst.RefreshToken, refreshToken);

            var token = RestPost<AuthToken>(req);
            return token;
        }
    }
}