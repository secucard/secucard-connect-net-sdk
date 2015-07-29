namespace Secucard.Connect.Auth
{
    using System;
    using System.Globalization;
    using System.Threading;
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
                try
                {
                    var newToken = Rest.RefreshToken(token.RefreshToken);

                    token.AccessToken = newToken.AccessToken;
                    token.ExpiresIn = newToken.ExpiresIn;
                    if (!string.IsNullOrEmpty(newToken.RefreshToken)) token.RefreshToken = newToken.RefreshToken;
                    token.SetExpireTime();
                    StoreToken(token);
                    TraceInfo("Token refreshed and returned: {0}", token);
                }
                catch (Exception ex)
                {
                    // refreshing failed, clear the token
                    Storage.Clear(GetTokenStoreId(), null);
                    token = null;
                    TraceInfo("Token refreshed failed");
                }
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
                TraceInfo("Retrieved codes for device auth: {0}, now polling for auth.", codes);
                token = PollToken(codes);
            }
            else // USER
            {
                token = Rest.GetToken();
            }

            TraceInfo("New token retrieved: {0}", token.ToString());

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
            if (seconds <= 0 || Config.WaitTimeoutSec < seconds)
            {
                seconds = Config.WaitTimeoutSec;
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

        private void TraceInfo(string fmt, params object[] param )
        {
            if(Trace!=null) Trace.Info(fmt,param);
        }

    }
}