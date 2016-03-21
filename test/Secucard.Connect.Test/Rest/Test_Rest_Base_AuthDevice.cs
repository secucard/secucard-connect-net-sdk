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

namespace Secucard.Connect.Test.Rest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Storage;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Base_AuthDevice : Test_Base
    {
        protected readonly RestService RestService;
        protected readonly IClientAuthDetails ClientAuthDetailsDevice;

        public Test_Rest_Base_AuthDevice()
        {
            properties["Auth.ExtendExpire"] = "true";
            var clientConfig = new ClientConfiguration(properties)
            {
                DataStorage = new MemoryDataStorage()
            };
            SecucardConnect.Create(clientConfig);

            AuthConfig = new AuthConfig(properties);

            ClientAuthDetailsDevice = new ClientAuthDetailsDeviceTest();

            var authProvider = new TokenManager(AuthConfig, ClientAuthDetailsDevice, new RestAuth(AuthConfig));

            authProvider.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;
            AccessToken = authProvider.GetToken(true);

            RestService = new RestService(RestConfig);
        }

        private void TokenManagerOnTokenManagerStatusUpdateEvent(object sender, TokenManagerStatusUpdateEventArgs args)
        {
            if (args.Status == AuthStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = new Uri(AuthConfig.OAuthUrl).Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin {UserPin = args.DeviceAuthCodes.UserCode})
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart =
                    new RestService(new RestConfig { Url = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}