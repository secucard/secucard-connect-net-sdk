namespace Secucard.Connect.Auth
{
    using System;
    using System.Threading;
    using Secucard.Connect.Auth.Exception;
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Client;

    /// <summary>
    ///     Implementation of the TokenManager which gets an OAuth token via REST.
    ///     The retrieved token is also cached and refreshed.
    /// </summary>
    public class TokenManager
    {
        public event TokenManagerStatusUpdateEventHandler TokenManagerStatusUpdateEvent;

        public ClientContext Context { get; set; }

        private readonly AuthConfig Config;
        private readonly string Id;
        private readonly RestAuth Rest;
        private bool CancelAuthFlag { get; set; }
        private IClientAuthDetails ClientAuthDetails { get; set; }

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
                    catch (System.Exception ex)
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
            if (credentials == null) { throw new System.Exception("Missing credentials"); }

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
                if (TokenManagerStatusUpdateEvent != null)
                    TokenManagerStatusUpdateEvent.Invoke(this,
                        new TokenManagerStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthStatusEnum.Pending
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
                            OnTokenManagerStatusUpdateEvent(new TokenManagerStatusUpdateEventArgs
                            {
                                DeviceAuthCodes = codes,
                                Status = AuthStatusEnum.Pending
                            });
                        }
                    }
                    else
                    {
                        var clientCredentials = credentials as ClientCredentials;
                        token = Rest.GetToken(clientCredentials.ClientId, clientCredentials.ClientSecret);
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                if (token != null)
                {
                    OnTokenManagerStatusUpdateEvent(new TokenManagerStatusUpdateEventArgs
                            {
                                DeviceAuthCodes = codes,
                                Status = AuthStatusEnum.Ok,
                                Token = token
                            });
                    return token;
                }

            } while (DateTime.Now < timeout);

            if (isDeviceAuth)
            {
                throw new AuthTimeoutException();
            }

            throw new System.Exception("Unexpected failure of authentication.");
        }

        private void OnTokenManagerStatusUpdateEvent(TokenManagerStatusUpdateEventArgs args)
        {
            if (TokenManagerStatusUpdateEvent != null) TokenManagerStatusUpdateEvent.Invoke(this, args);
        }
 

        private void SetCurrentToken(Token token)
        {
            if (ClientAuthDetails != null) ClientAuthDetails.OnTokenChanged(token);
        }

        private void TraceInfo(string fmt, params object[] param)
        {
            if (Context.SecucardTrace != null) Context.SecucardTrace.Info(fmt, param);
        }

    }
}