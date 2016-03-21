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
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp.Client;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Test.Rest;

    [TestClass]
    public class Test_RestAuth : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Auth")]
        public void Test_Auth_Device_Start()
        {
            var request = new RestRequest
            {
                Host = new Uri(AuthConfig.OAuthUrl).Host
            };

            request.BodyParameter.Add(AuthConst.ClientId, ClientAuthDetailsDevice.GetClientCredentials().ClientId);
            request.BodyParameter.Add(AuthConst.ClientSecret, ClientAuthDetailsDevice.GetClientCredentials().ClientSecret);
            request.BodyParameter.Add(AuthConst.GrantType, RestAuth.Device);
            request.BodyParameter.Add(AuthConst.Uuid, (ClientAuthDetailsDevice.GetCredentials() as DeviceCredentials).DeviceId);

            var rest = new RestAuth(AuthConfig);
            rest.RestPost(request);
        }

        [TestMethod, TestCategory("Auth")]
        public void Test_Auth_Until_AccessToken()
        {
            var rest = new RestAuth(AuthConfig);

            // AUTH: GetToken
            var reqDeviceGetToken = new RestRequest
            {
                Host = new Uri(AuthConfig.OAuthUrl).Host
            };

            reqDeviceGetToken.BodyParameter.Add(AuthConst.GrantType, RestAuth.Device);
            reqDeviceGetToken.BodyParameter.Add(AuthConst.ClientId, ClientAuthDetailsDevice.GetClientCredentials().ClientId);
            reqDeviceGetToken.BodyParameter.Add(AuthConst.ClientSecret,
                ClientAuthDetailsDevice.GetClientCredentials().ClientSecret);
            reqDeviceGetToken.BodyParameter.Add(AuthConst.Uuid, (ClientAuthDetailsDevice.GetCredentials() as DeviceCredentials).DeviceId);

            var ret = rest.RestPost(reqDeviceGetToken);
            var authDeviceGetTokenOut = JsonSerializer.DeserializeJson<DeviceAuthCode>(ret);

            Assert.AreEqual(authDeviceGetTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceGetTokenOut.Interval, 5);
            Assert.AreEqual(authDeviceGetTokenOut.VerificationUrl, VerificationUrl);


            // Set pin via SMART REST (only development)

            var restSmart =
                    new RestService(new RestConfig { Url = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });

            var reqSmartPin = new RestRequest
            {
                Method = WebRequestMethods.Http.Post,
                PageUrl = "", //ConfigAuth.PageSmartDevices,
                Host = new Uri(AuthConfig.OAuthUrl).Host,
                BodyJsonString = JsonSerializer.SerializeJson(new SmartPin {UserPin = authDeviceGetTokenOut.UserCode})
            };

            reqDeviceGetToken.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
            var response = restSmart.RestPut(reqSmartPin);
            Assert.IsTrue(response.Length > 0);
            // No need to validate response. Call needed to set PIN


            // AUTH: Obtain Access Token
            var reqObtainAccessToken = new RestRequest
            {
                Host = new Uri(AuthConfig.OAuthUrl).Host
            };

            reqObtainAccessToken.BodyParameter.Add(AuthConst.GrantType, RestAuth.Device);
            reqObtainAccessToken.BodyParameter.Add(AuthConst.ClientId,
                ClientAuthDetailsDevice.GetClientCredentials().ClientId);
            reqObtainAccessToken.BodyParameter.Add(AuthConst.ClientSecret,
                ClientAuthDetailsDevice.GetClientCredentials().ClientSecret);
            reqObtainAccessToken.BodyParameter.Add(AuthConst.Code, authDeviceGetTokenOut.DeviceCode);

            ret = rest.RestPost(reqObtainAccessToken);
            var authDeviceTokenOut = JsonSerializer.DeserializeJson<Token>(ret);


            Assert.AreEqual(authDeviceTokenOut.TokenType, TokenTypeBearer);
            Assert.AreEqual(authDeviceTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceTokenOut.RefreshToken.Length, 40);


            // Refresh Token
            var reqRefreshExpiredToken = new RestRequest
            {
                Host = new Uri(AuthConfig.OAuthUrl).Host
            };

            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.GrantType, RestAuth.Refreshtoken);
            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.ClientId,
                ClientAuthDetailsDevice.GetClientCredentials().ClientId);
            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.ClientSecret,
                ClientAuthDetailsDevice.GetClientCredentials().ClientSecret);
            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.RefreshToken, authDeviceTokenOut.RefreshToken);


            ret = rest.RestPost(reqRefreshExpiredToken);
            var authRefreshTokenOut = JsonSerializer.DeserializeJson<Token>(ret);

            Assert.AreEqual(authRefreshTokenOut.AccessToken.Length, 26);
            Assert.AreEqual(authRefreshTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authRefreshTokenOut.TokenType, TokenTypeBearer);

            Debug.WriteLine(authRefreshTokenOut.AccessToken);

            using (var client = new StompClient(StompConfig))
            {
                var connect = client.Connect(authRefreshTokenOut.AccessToken, authRefreshTokenOut.AccessToken);
                Assert.IsTrue(connect);
                Assert.AreEqual(client.StompClientStatus, EnumStompClientStatus.Connected);

                var framePing = new StompFrame(StompCommands.Send);
                framePing.Headers.Add(StompHeader.UserId, authRefreshTokenOut.AccessToken);
                framePing.Headers.Add(StompHeader.Destination, "/exchange/connect.api/ping");
                framePing.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                framePing.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");

                framePing.Body = "Testdata";

                StompFrame frameIn = null;
                client.StompClientFrameArrivedEvent += (sender, args) => { frameIn = args.Frame; };
                client.SendFrame(framePing);

                // waiting for frame to come
                while (frameIn == null)
                {
                }

                Assert.IsTrue(frameIn.Body.Contains("Testdata"));

                // check out heartbeat in trace
                Thread.Sleep(6000);

                client.Disconnect();
                Thread.Sleep(3000); // Wait for Disconnect Receipt to arrive
                Assert.IsTrue(client.StompClientStatus == EnumStompClientStatus.Disconnected);
            }
        }

        [TestMethod, TestCategory("Auth")]
        public void Test_RestAuth_Until_AccessToken()
        {
            var rest = new RestAuth(AuthConfig);

            var authDeviceGetTokenOut = rest.GetDeviceAuthCode(ClientAuthDetailsDevice.GetClientCredentials().ClientId,
                ClientAuthDetailsDevice.GetClientCredentials().ClientSecret, (ClientAuthDetailsDevice.GetCredentials() as DeviceCredentials).DeviceId);
            Assert.AreEqual(authDeviceGetTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceGetTokenOut.Interval, 5);
            Assert.AreEqual(authDeviceGetTokenOut.VerificationUrl, VerificationUrl);


            // Set pin via SMART REST (only development)
            var reqSmartPin = new RestRequest
            {
                PageUrl = "", //ConfigAuth.PageSmartDevices,
                Host = new Uri(AuthConfig.OAuthUrl).Host,
                BodyJsonString = JsonSerializer.SerializeJson(new SmartPin {UserPin = authDeviceGetTokenOut.UserCode})
            };

            reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
            var restSmart =
                    new RestService(new RestConfig { Url = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });

            var response = restSmart.RestPut(reqSmartPin);
            Assert.IsTrue(response.Length > 0);
            // No need to validate response. Call needed to set PIN

            var authDeviceTokenOut = rest.ObtainAuthToken(authDeviceGetTokenOut.DeviceCode,
                ClientAuthDetailsDevice.GetClientCredentials().ClientId,
                ClientAuthDetailsDevice.GetClientCredentials().ClientSecret);
            Assert.AreEqual(authDeviceTokenOut.TokenType, TokenTypeBearer);
            Assert.AreEqual(authDeviceTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceTokenOut.RefreshToken.Length, 40);

            var authRefreshTokenOut = rest.RefreshToken(authDeviceTokenOut.RefreshToken,
                ClientAuthDetailsDevice.GetClientCredentials().ClientId,
                ClientAuthDetailsDevice.GetClientCredentials().ClientSecret);
            Assert.AreEqual(authRefreshTokenOut.AccessToken.Length, 26);
            Assert.AreEqual(authRefreshTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authRefreshTokenOut.TokenType, TokenTypeBearer);

            Debug.WriteLine(authRefreshTokenOut.AccessToken);
        }

        #region ### Const ###

        //// TODO: Will have to move to config files

        //// https://core-dev10.secupay-ag.de/app.core.connector/oauth/token

        //// private const string AuthUrl = "https://connect.secucard.com/";
        //private const string AuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/";

        //private const string ApiUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/";
        //// private const string ApiUrl = "https://connect.secucard.com/api/v2/";

        //private const string Host = "core-dev10.secupay-ag.de";
        //// private const string Host = "connect.secucard.com";

        //private const string ClientId = "611c00ec6b2be6c77c2338774f50040b";
        //private const string Secret = "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb";
        ////private const string Uuid = "/vendor/unknown/cashier/iostest1";
        //private const string Uuid = "/vendor/unknown/cashier/dotnettest1";

        ////private const string PageOauthToken = "oauth/token";
        //private const string PageOauthToken = "oauth/token";

        //private const string PageSmartDevices ="api/v2/Smart/Devices/SDV_2FUFB3YJQ2YBHEDJKBSA9Q57NM8UA6/pin";

        // TEST Const
        private const string VerificationUrl = "http://www.secuoffice.com";
        private const string TokenTypeBearer = "bearer";

        #endregion
    }
}