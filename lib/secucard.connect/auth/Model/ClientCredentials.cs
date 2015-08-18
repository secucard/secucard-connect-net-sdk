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

namespace Secucard.Connect.Auth.Model
{
    using System.Collections.Generic;

    public class ClientCredentials : OAuthCredentials
    {
        public ClientCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public override string GrantType
        {
            get { return "client_credentials"; }
        }

        public override string Id
        {
            get { return GrantType + ClientId + ClientSecret; }
        }

        public override Dictionary<string, object> AsMap()
        {
            var map = base.AsMap();
            map.Add("client_id", ClientId);
            map.Add("client_secret", ClientSecret);
            return map;
        }
    }
}