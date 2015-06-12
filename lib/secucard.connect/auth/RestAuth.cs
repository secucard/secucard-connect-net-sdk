namespace secucard.connect
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Model.Auth;

    public class RestAuth : RestBase
    {
        private readonly AuthConfig Config;

        public RestAuth(AuthConfig config)
        {
            Config = config;
        }

        public DeviceAuthCode GetDeviceAuthCode()
        {
            var req = new RestRequest(Config.AuthUrl)
            {
                PageUrl = Config.PageOauthToken,
                Host = Config.Host
            };

            req.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            req.Parameter.Add(AuthConst.Client_Id, Config.ClientCredentials.ClientId);
            req.Parameter.Add(AuthConst.Client_Secret, Config.ClientCredentials.ClientSecret);
            req.Parameter.Add(AuthConst.Uuid, Config.Uuid);

            var deviceAuthCode = RestPost<DeviceAuthCode>(req);
            return deviceAuthCode;
        }

        public AuthToken ObtainAuthToken(string deviceCode)
        {
            var req = new RestRequest(Config.AuthUrl)
            {
                PageUrl = Config.PageOauthToken,
                Host = Config.Host
            };

            req.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            req.Parameter.Add(AuthConst.Client_Id, Config.ClientId);
            req.Parameter.Add(AuthConst.Client_Secret, Config.Secret);
            req.Parameter.Add(AuthConst.Code, deviceCode);

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
            var req = new RestRequest(Config.AuthUrl)
            {
                PageUrl = Config.PageOauthToken,
                Host = Config.Host
            };

            req.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.RrefreshToken);
            req.Parameter.Add(AuthConst.Client_Id, Config.ClientId);
            req.Parameter.Add(AuthConst.Client_Secret, Config.Secret);
            req.Parameter.Add(AuthConst.RefreshToken, refreshToken);

            var token = RestPost<AuthToken>(req);
            return token;
        }
    }
}