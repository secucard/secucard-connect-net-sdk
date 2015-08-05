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

namespace Secucard.Connect.auth.Model
{
    using System.Collections.Generic;
    using Secucard.Connect.Auth;

    public class DeviceCredentials : ClientCredentials
    {
        public DeviceCredentials(string clientId, string clientSecret, string deviceId) :
            base(clientId, clientSecret)
        {
            DeviceId = deviceId;
        }

        public DeviceCredentials(ClientCredentials clientCredentials, string deviceId)
            : this(clientCredentials.ClientId, clientCredentials.ClientSecret, deviceId)
        {
        }

        //   Code obtained during the authorization process
        public string DeviceCode { get; set; }
        //A unique device id like UUID.
        public string DeviceId { get; set; }

        public override string Id
        {
            get { return GrantType + ClientId + ClientSecret + DeviceId; }
        }

        public override string GrantType
        {
            get { return "device"; }
        }

        public override Dictionary<string, object> AsMap()
        {
            var map = base.AsMap();
            if (DeviceId != null)
                map.Add("uuid", DeviceId);

            if (DeviceCode != null)
                map.Add("code", DeviceCode);
            return map;
        }
    }
}