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

namespace Secucard.Connect.Test.Auth
{
    using System;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Test.Rest;

    [TestClass]
    public class Test_OAuthProvider : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("OAuthProvider")]
        [DeploymentItem("data", "data")]
        public void Test_OAuthDevice()
        {
            var restAuth = new RestAuth(AuthConfig);

            // first run with empty storage
            var tokenManager = new TokenManager(AuthConfig, ClientAuthDetailsDevice, restAuth);

            tokenManager.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;
            var token = tokenManager.GetToken(true);
            Assert.IsNotNull(token);

            // second run with token in storage still valid
            tokenManager = new TokenManager(AuthConfig, ClientAuthDetailsDevice, restAuth);
            tokenManager.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;
            token = tokenManager.GetToken(true);
            Assert.IsNotNull(token);

            tokenManager = new TokenManager(AuthConfig, ClientAuthDetailsDevice, restAuth);
            tokenManager.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;
            token = tokenManager.GetToken(true);
            Assert.IsNotNull(token);
        }

        private void TokenManagerOnTokenManagerStatusUpdateEvent(object sender, TokenManagerStatusUpdateEventArgs args)
        {
            if (args.Status == AuthStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Method = WebRequestMethods.Http.Post,
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