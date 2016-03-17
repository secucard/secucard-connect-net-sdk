/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
        private readonly AuthConfig Config;
        private readonly RestAuth Rest;

        public TokenManager(AuthConfig config, IClientAuthDetails clientAuthDetails, RestAuth restAuth)
        {
            Config = config;
            ClientAuthDetails = clientAuthDetails;
            Rest = restAuth;
        }

        private bool CancelAuthFlag { get; set; }
        private IClientAuthDetails ClientAuthDetails { get; set; }
        public event TokenManagerStatusUpdateEventHandler TokenManagerStatusUpdateEvent;

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

            var authenticate = false;

            if (token == null)
            {
                // no token, authenticate first
                authenticate = true;
            }
            else if (token.IsExpired())
            {
                // try refresh if just expired, authenticate new if no refresh possible or failed
                SecucardTrace.InfoSource("Token expired: {0} , original:{1}",
                    token.ExpireTime == null ? "null" : token.ExpireTime.Value.ToString(),
                    token.OrigExpireTime == null ? "null" : token.OrigExpireTime.ToString());
                if (token.RefreshToken == null)
                {
                    SecucardTrace.Info("No token refresh possible, try obtain new.");
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
                        SecucardTrace.Info("Token refresh failed, try obtain new. {0}", ex);
                        authenticate = true;
                    }
                }
            }
            else
            {
                // we should have valid token in cache, no new auth necessary
                if (Config.ExtendExpire)
                {
                    SecucardTrace.Info("Extend token expire time.");
                    token.SetExpireTime();
                    SetCurrentToken(token);
                }
                SecucardTrace.Info("Return current token: {0}", token);
            }

            if (authenticate)
            {
                var credentials = ClientAuthDetails.GetCredentials();

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
                SecucardTrace.Info("Return new token: {0}", token);
            }

            return token.AccessToken;
        }

        private void Refresh(Token token, ClientCredentials credentials)
        {
            if (credentials == null)
            {
                throw new System.Exception("Missing credentials");
            }

            SecucardTrace.Info("Refresh token: {0}", credentials);
            var refreshToken = Rest.RefreshToken(token.RefreshToken, credentials.ClientId, credentials.ClientSecret);
            token.AccessToken = refreshToken.AccessToken;
            token.ExpiresIn = refreshToken.ExpiresIn;
            if (!string.IsNullOrWhiteSpace(refreshToken.RefreshToken)) token.RefreshToken = refreshToken.RefreshToken;
            token.SetExpireTime();
        }

        private Token Authenticate(OAuthCredentials credentials)
        {
            if (credentials == null)
            {
                throw new AuthFailedException("Missing credentials");
            }

            SecucardTrace.Info("Authenticate credentials: {0}", credentials.ToString());

            var pollInterval = 0;
            var timeout = DateTime.Now;
            var devicesCredentials = credentials as DeviceCredentials;
            var isDeviceAuth = (devicesCredentials != null);
            DeviceAuthCode codes = null;


            // if DeviceAuth then get codes an pass to app thru event. Further action required by client
            if (isDeviceAuth)
            {
                codes = Rest.GetDeviceAuthCode(devicesCredentials.ClientId, devicesCredentials.ClientSecret, devicesCredentials.DeviceId);
                if (TokenManagerStatusUpdateEvent != null)
                    TokenManagerStatusUpdateEvent.Invoke(this,
                        new TokenManagerStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthStatusEnum.Pending
                        });

                SecucardTrace.Info("Retrieved codes for device auth: {0}, now polling for auth.", codes);

                // set poll timeout, either by config or by expire time of code
                var t = codes.ExpiresIn;
                if (t <= 0 || Config.AuthWaitTimeoutSec < t)
                {
                    t = Config.AuthWaitTimeoutSec;
                }
                timeout = DateTime.Now.AddSeconds(t*1000);

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
                        Thread.Sleep(pollInterval*1000);

                        token = Rest.ObtainAuthToken(codes.DeviceCode, devicesCredentials.ClientId,
                            devicesCredentials.ClientSecret);
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
    }
}