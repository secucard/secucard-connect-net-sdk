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

    public class ClientAuthDetailsUserTest : AbstractClientAuthDetails, IClientAuthDetails
    {
        public OAuthCredentials GetCredentials()
        {
            return new AppUserCredentials(GetClientCredentials(), "f0478f73afe218e8b5f751a07c978ecf",
                "30644327cfbde722ad2ad12bb9c0a2f86a2bee0a2d8de8d862210112af3d01bb", "device");
        }

        public ClientCredentials GetClientCredentials()
        {
            return new ClientCredentials(
                "f0478f73afe218e8b5f751a07c978ecf",
                "30644327cfbde722ad2ad12bb9c0a2f86a2bee0a2d8de8d862210112af3d01bb");
        }
    }
}