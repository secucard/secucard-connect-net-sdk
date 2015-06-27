namespace Secucard.Connect.Auth
{
    using System;
    using System.Threading;
    using secucard.connect;
    using Secucard.Connect.auth;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Model.Auth;

    /// <summary>
    ///     Implementation of the AuthProvider interface which gets an OAuth token via REST channel.
    ///     The retrieved token is also cached and refreshed.
    /// </summary>
    public class AuthProvider : IAuthProvider
    {
        public delegate void AuthProviderStatusUpdateDelegate(object sender, AuthProviderStatusUpdateEventArgs args);

        private readonly AuthConfig Config;
        private readonly string Id;
        private readonly RestAuth Rest;
        private readonly DataStorage Storage;
        private readonly ISecucardTrace Trace;
        private bool CancelAuthFlag { get; set; }
        //private readonly UserAgentProvider userAgentProvider = new UserAgentProvider();

        public AuthProvider(string id, AuthConfig config, ISecucardTrace trace, DataStorage storage)
        {
            Id = id;
            Config = config;
            Trace = trace;
            Storage = storage;
            Rest = new RestAuth(config);
        }

        public void CancelAuth()
        {
            CancelAuthFlag = true;
        }

        public void ClearAuthCache()
        {
            Storage.Clear(GetTokenStoreId(), null);
        }

        //TODO: synchronized
        public AuthToken GetToken()
        {
            return GetToken(true);
        }

        //TODO: synchronized
        public AuthToken GetToken(bool extendToken)
        {
            var token = (AuthToken) Storage.Get(GetTokenStoreId());

            if (token != null && !token.IsExpired())
            {
                if (extendToken)
                {
                    // extend expire time on every token access, assuming the token is used, if not this could cause auth failure
                    token.SetExpireTime();
                    StoreToken(token);
                }
                return token;
            }

            if (token != null && token.RefreshToken != null)
            {
                // token is expired and can be refreshed without further auth.
                AuthToken newToken = null;
                try
                {
                    newToken = Rest.RefreshToken(token.RefreshToken);
                }
                finally
                {
                    if (newToken == null)
                    {
                        // refreshing failed, clear the token
                        Storage.Clear(GetTokenStoreId(), null);
                        token = null;
                    }
                    else
                    {
                        token.AccessToken = newToken.AccessToken;
                        token.ExpiresIn = newToken.ExpiresIn;
                        if (!string.IsNullOrEmpty(newToken.RefreshToken)) token.RefreshToken = newToken.RefreshToken;
                        token.SetExpireTime();
                        StoreToken(token);
                    }
                }
                Trace.Info("Token refreshed and returned: {0}", token);
            }
            else
            {
                token = null;
            }

            if (token != null) return token;

            // no token yet, a new one must be created
            if (Config.AuthType == AuthTypeEnum.Device)
            {
                var codes = Rest.GetDeviceAuthCode();
                if (AuthProviderStatusUpdate != null)
                    AuthProviderStatusUpdate.Invoke(this,
                        new AuthProviderStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthProviderStatusEnum.Pending
                        });
                Trace.Info("Retrieved codes for device auth: {0}, now polling for auth.", codes);
                token = PollToken(codes);
            }

            Trace.Info("New token retrieved: {0}", token.ToString());

            // set new expire time and store
            token.SetExpireTime();
            StoreToken(token);

            return token;
        }

        public event AuthProviderStatusUpdateDelegate AuthProviderStatusUpdate;

        private AuthToken PollToken(DeviceAuthCode codes)
        {
            // set poll timeout, either by config or by expire time of code
            var seconds = codes.ExpiresIn;
            if (seconds <= 0 || Config.AuthWaitTimeoutSec < seconds)
            {
                seconds = Config.AuthWaitTimeoutSec;
            }
            var timeout = DateTime.Now.AddSeconds(seconds);

            var pollIntervalInMs = codes.Interval*1000;
            if (pollIntervalInMs <= 0)
            {
                pollIntervalInMs = 5000; // poll default 5s
            }

            // reset flag to stop polling from external
            CancelAuthFlag = false;

            // poll server and send events to client accordingly
            while (DateTime.Now < timeout)
            {
                if (CancelAuthFlag)
                {
                    throw new AuthCanceledException("Authorization canceled by request.");
                }

                Thread.Sleep(pollIntervalInMs);

                var newToken = Rest.ObtainAuthToken(codes.DeviceCode);

                if (newToken != null)
                {
                    if (AuthProviderStatusUpdate != null)
                        AuthProviderStatusUpdate.Invoke(this,
                            new AuthProviderStatusUpdateEventArgs
                            {
                                DeviceAuthCodes = codes,
                                Status = AuthProviderStatusEnum.Ok
                            });
                    return newToken;
                }

                if (AuthProviderStatusUpdate != null)
                    AuthProviderStatusUpdate.Invoke(this,
                        new AuthProviderStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthProviderStatusEnum.Pending
                        });
            }

            throw new AuthCanceledException("Authorization canceled by timeout or authorization code was expired.");
        }

        private void StoreToken(AuthToken token)
        {
            Storage.Save(GetTokenStoreId(), token);
        }

        private string GetTokenStoreId()
        {
            return "token-" + Id;
        }


        //protected Dictionary<string, object> createAuthParams(ClientCredentials clientCredentials,
        //    UserCredentials userCredentials,
        //    string refreshToken, string deviceId, Dictionary<string, string> deviceInfo,
        //    string deviceCode)
        //{
        //    var parameters = new Dictionary<string, object>();

        //    // default type, client id / secret must always exist
        //    parameters.Add("grant_type", "client_credentials");
        //    parameters.Add("client_id", clientCredentials.ClientId);
        //    parameters.Add("client_secret", clientCredentials.ClientSecret);

        //    if (refreshToken != null)
        //    {
        //        parameters.Add("grant_type", "refresh_token");
        //        parameters.Add("refresh_token", refreshToken);
        //    }
        //    else if (userCredentials != null)
        //    {
        //        parameters.Add("grant_type", "appuser");
        //        parameters.Add("username", userCredentials.Username);
        //        parameters.Add("password", userCredentials.Password);
        //        if (deviceId != null) parameters.Add("device", deviceId);
        //        if (deviceInfo != null)
        //        {
        //            //parameters.Add(deviceInfo);}
        //        }
        //        else if ("device".Equals(Config.AuthType) && (deviceId != null || deviceCode != null))
        //        {
        //            parameters.Add("grant_type", "device");
        //            if (deviceId != null)
        //            {
        //                parameters.Add("uuid", deviceId);
        //            }
        //            if (deviceCode != null)
        //            {
        //                parameters.Add("code", deviceCode);
        //            }
        //        }
        //        return parameters;
        //    }
        //    return null;
        //}
    }
}