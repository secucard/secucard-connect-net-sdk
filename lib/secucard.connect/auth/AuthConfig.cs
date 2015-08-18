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
    using Secucard.Connect.Client.Config;

    public class AuthConfig
    {
        public string OAuthUrl { get; set; }
        public int AuthWaitTimeoutSec { get; set; }
        public bool ExtendExpire { get; set; }

        public AuthTypeEnum AuthType { get; set; }
        public string Host { get; set; }
        public string Uuid { get; set; }

        public AuthConfig(Properties properties)
        {
            OAuthUrl = properties.Get("Auth.OAuthUrl", "https://core-dev10.secupay-ag.de/app.core.connector/oauth/token");
            AuthWaitTimeoutSec = properties.Get("Auth.AuthWaitTimeoutSec", 240);
            ExtendExpire = properties.Get("Auth.ExtendExpire", true);

            AuthTypeEnum auth;
            if (AuthTypeEnum.TryParse(properties.Get("Auth.AuthType", "device"), true, out auth))
                AuthType = auth;
            else
            {
                AuthType = AuthTypeEnum.Device;
            }
            Host = properties.Get("Auth.Host", "/vendor/unknown/cashier/dotnettest1");
            Uuid = properties.Get("Auth.Uuid", "https://core-dev10.secupay-ag.de/app.core.connector/oauth/token");
        }
    }
}