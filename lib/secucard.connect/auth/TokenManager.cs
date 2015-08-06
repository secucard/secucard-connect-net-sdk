namespace Secucard.Connect.Auth
{
    using System;
    using System.Threading;
    using Secucard.Connect.auth;
    using Secucard.Connect.auth.Exception;
    using Secucard.Connect.auth.Model;
    using AuthToken = Secucard.Connect.auth.Model.Token;

    /// <summary>
    ///     Implementation of the AuthProvider interface which gets an OAuth token via REST channel.
    ///     The retrieved token is also cached and refreshed.
    /// </summary>
    public class TokenManager
    {
        public delegate void AuthProviderStatusUpdateDelegate(object sender, AuthProviderStatusUpdateEventArgs args);

        private readonly AuthConfig Config;
        private readonly string Id;
        private readonly RestAuth Rest;
        private bool CancelAuthFlag { get; set; }
        private IClientAuthDetails ClientAuthDetails { get; set; }
        public ClientContext Context { get; set; }

        public TokenManager(AuthConfig config, IClientAuthDetails clientAuthDetails, RestAuth restAuth)
        {
            Config = config;
            ClientAuthDetails = clientAuthDetails;
            Rest = restAuth;
        }


        private Token GetCurrent()
        {
            if (ClientAuthDetails != null)
            {
                return ClientAuthDetails.GetCurrent();
            }
            return null;
        }

        public string GetToken(bool allowInteractive)
        {
            var token = GetCurrent();

            bool authenticate = false;

            if (token == null)
            {
                // no token, authenticate first
                authenticate = true;
            }
            else if (token.IsExpired())
            {
                // try refresh if just expired, authenticate new if no refresh possible or failed
                TraceInfo("Token expired: {0} , original:{1}",
                      token.ExpireTime == null ? "null" : token.ExpireTime.Value.ToString(),
                      token.OrigExpireTime == null ? "null" : token.OrigExpireTime.ToString());
                if (token.RefreshToken == null)
                {
                    TraceInfo("No token refresh possible, try obtain new.");
                    authenticate = true;
                }
                else
                {
                    try
                    {
                        Refresh(token, ClientAuthDetails.GetClientCredentials());
                        SetCurrentToken(token);
                    }
                    catch (Exception ex)
                    {
                        TraceInfo("Token refresh failed, try obtain new. {0}", ex);
                        authenticate = true;
                    }
                }
            }
            else
            {
                // we should have valid token in cache, no new auth necessary
                if (Config.ExtendExpire)
                {
                    TraceInfo("Extend token expire time.");
                    token.SetExpireTime();
                    SetCurrentToken(token);
                }
                TraceInfo("Return current token: ", token);
            }

            if (authenticate)
            {
                OAuthCredentials credentials = ClientAuthDetails.GetCredentials();

                if (credentials is AnonymousCredentials)
                {
                    return null;
                }

                // new authentication is needed but only if allowed
                if ((credentials is AppUserCredentials || credentials is DeviceCredentials) && !allowInteractive)
                {
                    throw new AuthFailedException("Invalid access token, please authenticate again.");
                }

                token = Authenticate(credentials);
                token.SetExpireTime();
                token.Id = credentials.Id;
                SetCurrentToken(token);
                TraceInfo("Return new token: {0}", token);
            }

            return token.AccessToken;
        }

        private void Refresh(Token token, ClientCredentials credentials)
        {
            if (credentials == null) { throw new Exception("Missing credentials"); }

            TraceInfo("Refresh token: {0}", credentials);
            Token refreshToken = Rest.RefreshToken(token.RefreshToken, credentials.ClientId, credentials.ClientSecret);
            token.AccessToken = refreshToken.AccessToken;
            token.ExpiresIn = refreshToken.ExpiresIn;
            if (!string.IsNullOrWhiteSpace(refreshToken.RefreshToken)) token.RefreshToken = refreshToken.RefreshToken;
            token.SetExpireTime();
        }

        private Token Authenticate(OAuthCredentials credentials)
        {
            if (credentials == null) { throw new AuthFailedException("Missing credentials"); }

            TraceInfo("Authenticate credentials: {0}", credentials.AsMap());

            int pollInterval = 0;
            DateTime timeout = DateTime.Now;
            var devicesCredentials = credentials as DeviceCredentials;
            bool isDeviceAuth = (devicesCredentials != null);
            DeviceAuthCode codes = null;


            // if DeviceAuth then get codes an pass to app thru event. Further Action required by client
            if (isDeviceAuth)
            {
                codes = Rest.GetDeviceAuthCode(devicesCredentials.ClientId, devicesCredentials.ClientSecret);
                if (AuthProviderStatusUpdate != null)
                    AuthProviderStatusUpdate.Invoke(this,
                        new AuthProviderStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthProviderStatusEnum.Pending
                        });

                TraceInfo("Retrieved codes for device auth: {0}, now polling for auth.", codes);

                // set poll timeout, either by config or by expire time of code
                int t = codes.ExpiresIn;
                if (t <= 0 || Config.AuthWaitTimeoutSec < t)
                {
                    t = Config.AuthWaitTimeoutSec;
                }
                timeout = DateTime.Now.AddSeconds(t * 1000);

                pollInterval = codes.Interval;
                if (pollInterval <= 0)
                {
                    pollInterval = 5; // poll default 5s
                }

                devicesCredentials.DeviceCode = codes.DeviceCode;
                devicesCredentials.DeviceId = null; // device id must be empty for next auth. step!
            }


            do
            {
                Token token;
                try
                {
                    if (isDeviceAuth)
                    {
                        // in case of device auth, check for cancel and delay polling
                        if (CancelAuthFlag) throw new AuthCanceledException("Authorization canceled by request.");
                        Thread.Sleep(pollInterval * 1000);

                        token = Rest.ObtainAuthToken(codes.DeviceCode, devicesCredentials.ClientId, devicesCredentials.ClientSecret);
                        if (token == null) // auth not completed yet
                        {
                            FireEvent(new AuthProviderStatusUpdateEventArgs
                            {
                                DeviceAuthCodes = codes,
                                Status = AuthProviderStatusEnum.Pending
                            });
                        }
                    }
                    else
                    {
                        var clientCredentials = credentials as ClientCredentials;
                        token = Rest.GetToken(clientCredentials.ClientId, clientCredentials.ClientSecret);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (token != null)
                {
                    FireEvent(new AuthProviderStatusUpdateEventArgs
                            {
                                DeviceAuthCodes = codes,
                                Status = AuthProviderStatusEnum.Ok,
                                Token = token
                            });
                    return token;
                }

            } while (DateTime.Now < timeout);

            if (isDeviceAuth)
            {
                throw new AuthTimeoutException();
            }

            throw new Exception("Unexpected failure of authentication.");
        }

        private void FireEvent(AuthProviderStatusUpdateEventArgs args)
        {
            if (AuthProviderStatusUpdate != null) AuthProviderStatusUpdate.Invoke(this, args);
        }




        //public AuthToken GetToken(bool extendToken)
        //{
        //    var token = ClientAuthDetails.GetCurrent();

        //    if (token != null && !token.IsExpired())
        //    {
        //        if (extendToken)
        //        {
        //            // extend expire time on every token access, assuming the token is used, if not this could cause auth failure
        //            token.SetExpireTime();
        //            StoreToken(token);
        //        }
        //        return token;
        //    }

        //    if (token != null && token.RefreshToken != null)
        //    {
        //        // token is expired and can be refreshed without further auth.
        //        try
        //        {
        //            var newToken = Rest.RefreshToken(token.RefreshToken, ClientAuthDetails.GetClientCredentials().ClientId, ClientAuthDetails.GetClientCredentials().ClientSecret);

        //            token.AccessToken = newToken.AccessToken;
        //            token.ExpiresIn = newToken.ExpiresIn;
        //            if (!string.IsNullOrEmpty(newToken.RefreshToken)) token.RefreshToken = newToken.RefreshToken;
        //            token.SetExpireTime();
        //            StoreToken(token);
        //            TraceInfo("Token refreshed and returned: {0}", token);
        //        }
        //        catch (Exception ex)
        //        {
        //            // refreshing failed, clear the token
        //            //Storage.Clear(GetTokenStoreId(), null);
        //            token = null;
        //            TraceInfo("Token refreshed failed");
        //        }
        //    }
        //    else
        //    {
        //        token = null;
        //    }

        //    if (token != null) return token;

        //    // no token yet, a new one must be created
        //    if (Config.AuthType == AuthTypeEnum.Device)
        //    {
        //        var codes = Rest.GetDeviceAuthCode(ClientAuthDetails.GetClientCredentials().ClientId, ClientAuthDetails.GetClientCredentials().ClientSecret);
        //        if (AuthProviderStatusUpdate != null)
        //            AuthProviderStatusUpdate.Invoke(this,
        //                new AuthProviderStatusUpdateEventArgs
        //                {
        //                    DeviceAuthCodes = codes,
        //                    Status = AuthProviderStatusEnum.Pending
        //                });
        //        TraceInfo("Retrieved codes for device auth: {0}, now polling for auth.", codes);
        //        token = PollToken(codes);
        //    }
        //    else // USER
        //    {
        //        token = Rest.GetToken(ClientAuthDetails.GetClientCredentials().ClientId, ClientAuthDetails.GetClientCredentials().ClientSecret);
        //    }

        //    TraceInfo("New token retrieved: {0}", token.ToString());

        //    // set new expire time and store
        //    token.SetExpireTime();
        //    StoreToken(token);

        //    return token;
        //}

        public event AuthProviderStatusUpdateDelegate AuthProviderStatusUpdate;

        //private AuthToken PollToken(DeviceAuthCode codes)
        //{
        //    // set poll timeout, either by config or by expire time of code
        //    var seconds = codes.ExpiresIn;
        //    if (seconds <= 0 || Config.AuthWaitTimeoutSec < seconds)
        //    {
        //        seconds = Config.AuthWaitTimeoutSec;
        //    }
        //    var timeout = DateTime.Now.AddSeconds(seconds);

        //    var pollIntervalInMs = codes.Interval * 1000;
        //    if (pollIntervalInMs <= 0)
        //    {
        //        pollIntervalInMs = 5000; // poll default 5s
        //    }

        //    // reset flag to stop polling from external
        //    CancelAuthFlag = false;

        //    // poll server and send events to client accordingly
        //    while (DateTime.Now < timeout)
        //    {
        //        if (CancelAuthFlag)
        //        {
        //            throw new AuthCanceledException("Authorization canceled by request.");
        //        }

        //        Thread.Sleep(pollIntervalInMs);

        //        var newToken = Rest.ObtainAuthToken(codes.DeviceCode, ClientAuthDetails.GetClientCredentials().ClientId, ClientAuthDetails.GetClientCredentials().ClientSecret);

        //        if (newToken != null)
        //        {
        //            if (AuthProviderStatusUpdate != null)
        //                AuthProviderStatusUpdate.Invoke(this,
        //                    new AuthProviderStatusUpdateEventArgs
        //                    {
        //                        DeviceAuthCodes = codes,
        //                        Status = AuthProviderStatusEnum.Ok
        //                    });
        //            return newToken;
        //        }

        //        if (AuthProviderStatusUpdate != null)
        //            AuthProviderStatusUpdate.Invoke(this,
        //                new AuthProviderStatusUpdateEventArgs
        //                {
        //                    DeviceAuthCodes = codes,
        //                    Status = AuthProviderStatusEnum.Pending
        //                });
        //    }

        //    throw new AuthCanceledException("Authorization canceled by timeout or authorization code was expired.");
        //}

        private void SetCurrentToken(AuthToken token)
        {
            if (ClientAuthDetails != null) ClientAuthDetails.OnTokenChanged(token);
        }

        private void TraceInfo(string fmt, params object[] param)
        {
            if (Context.SecucardTrace != null) Context.SecucardTrace.Info(fmt, param);
        }

    }
}