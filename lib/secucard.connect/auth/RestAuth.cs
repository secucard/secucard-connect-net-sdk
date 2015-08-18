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
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;

    public class RestAuth : RestBase
    {
        public const string DEVICE = "device";
        public const string REFRESHTOKEN = "refresh_token";
        public const string CLIENTCREDENTIALS = "client_credentials";

        private readonly AuthConfig AuthConfig;

        public RestAuth(AuthConfig authConfig)
            : base(authConfig.OAuthUrl)
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

            req.BodyParameter.Add(AuthConst.Grant_Type, DEVICE);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);
            req.BodyParameter.Add(AuthConst.Uuid, AuthConfig.Uuid);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<DeviceAuthCode>(ret);
            ;
        }

        public Token GetToken(string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, CLIENTCREDENTIALS);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<Token>(ret);
        }

        public Token ObtainAuthToken(string deviceCode, string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.Grant_Type, DEVICE);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);
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
                Host = AuthConfig.Host,
                UserAgent = UserAgentInfo
            };
            req.BodyParameter.Add(AuthConst.Grant_Type, REFRESHTOKEN);
            req.BodyParameter.Add(AuthConst.Client_Id, clientId);
            req.BodyParameter.Add(AuthConst.Client_Secret, clientSecret);
            req.BodyParameter.Add(AuthConst.RefreshToken, refreshToken);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<Token>(ret);
        }
    }
}