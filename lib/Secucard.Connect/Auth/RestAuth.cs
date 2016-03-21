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
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;

    public class RestAuth : RestBase
    {
        public const string Device = "device";
        public const string Refreshtoken = "refresh_token";
        public const string Clientcredentials = "client_credentials";

        private readonly string _host;

        public RestAuth(AuthConfig authConfig)
            : base(new RestConfig { Url = authConfig.OAuthUrl, ConnectTimeoutSec = authConfig.AuthWaitTimeoutSec })
        {
            _host = new Uri(authConfig.OAuthUrl).Host;
        }

        public string UserAgentInfo { get; set; }

        public DeviceAuthCode GetDeviceAuthCode(string clientId, string clientSecret, string uuid)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.GrantType, Device);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);
            req.BodyParameter.Add(AuthConst.Uuid, uuid);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<DeviceAuthCode>(ret);
        }

        public Token GetToken(string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.GrantType, Clientcredentials);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<Token>(ret);
        }

        public Token ObtainAuthToken(string deviceCode, string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.GrantType, Device);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);
            req.BodyParameter.Add(AuthConst.Code, deviceCode);

            try
            {
                var ret = RestPost(req);
                return JsonSerializer.DeserializeJson<Token>(ret);
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
                Host = _host,
                UserAgent = UserAgentInfo
            };
            req.BodyParameter.Add(AuthConst.GrantType, Refreshtoken);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);
            req.BodyParameter.Add(AuthConst.RefreshToken, refreshToken);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<Token>(ret);
        }
    }
}