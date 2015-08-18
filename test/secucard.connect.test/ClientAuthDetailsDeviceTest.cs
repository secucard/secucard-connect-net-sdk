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

namespace Secucard.Connect.Test
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Auth.Model;

    public class ClientAuthDetailsDeviceTest : AbstractClientAuthDetails, IClientAuthDetails
    {
        public OAuthCredentials GetCredentials()
        {
            return new DeviceCredentials("611c00ec6b2be6c77c2338774f50040b",
                "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb", "device");
        }

        public ClientCredentials GetClientCredentials()
        {
            return (ClientCredentials) GetCredentials();
        }
    }
}