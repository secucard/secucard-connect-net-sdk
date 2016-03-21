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
    public class AppUserCredentials : ClientCredentials
    {
        public AppUserCredentials(string clientId, string clientSecret, string userName, string password,
            string deviceId) :
            base(clientId, clientSecret)
        {
            UserName = userName;
            Password = password;
            DeviceId = deviceId;
        }

        public AppUserCredentials(ClientCredentials clientCredentials, string userName, string password, string deviceId)
            :
                this(clientCredentials.ClientId, clientCredentials.ClientSecret, userName, password, deviceId)
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }


        /// <summary>
        /// A unique device id like UUID. May be optional for some credential types.
        /// </summary>
        public string DeviceId { get; set; }
        
        public override string Id
        {
            get { return "appuser-" + ClientId + ClientSecret + UserName + Password + (DeviceId ?? ""); }
        }
    }
}