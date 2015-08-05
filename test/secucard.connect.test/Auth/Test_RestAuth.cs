namespace Secucard.Connect.Test
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using secucard.connect.test.Rest;
    using Secucard.Connect.auth;
    using Secucard.Connect.auth.Model;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.Auth;
    using Secucard.Model.Smart;
    using Secucard.Stomp;

    [TestClass]
    public class Test_RestAuth : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Auth")]
        public void Test_Auth_Device_Start()
        {
            var request = new RestRequest
            {
                Host = ConfigAuth.Host
            };

            request.BodyParameter.Add(AuthConst.Client_Id, ConfigAuth.ClientCredentials.ClientId);
            request.BodyParameter.Add(AuthConst.Client_Secret, ConfigAuth.ClientCredentials.ClientSecret);
            request.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            request.BodyParameter.Add(AuthConst.Uuid, ConfigAuth.Uuid);

            var rest = new RestAuth(ConfigAuth);
            rest.RestPost(request);
        }

        [TestMethod, TestCategory("Auth")]
        public void Test_Auth_Until_AccessToken()
        {
            var rest = new RestAuth(ConfigAuth);

            // AUTH: GetToken
            var reqDeviceGetToken = new RestRequest
            {
                Host = ConfigAuth.Host
            };

            reqDeviceGetToken.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            reqDeviceGetToken.BodyParameter.Add(AuthConst.Client_Id, ConfigAuth.ClientCredentials.ClientId);
            reqDeviceGetToken.BodyParameter.Add(AuthConst.Client_Secret, ConfigAuth.ClientCredentials.ClientSecret);
            reqDeviceGetToken.BodyParameter.Add(AuthConst.Uuid, ConfigAuth.Uuid);

            var authDeviceGetTokenOut = rest.RestPost<DeviceAuthCode>(reqDeviceGetToken);

            Assert.AreEqual(authDeviceGetTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceGetTokenOut.Interval, 5);
            Assert.AreEqual(authDeviceGetTokenOut.VerificationUrl, VerificationUrl);


            // Set pin via SMART REST (only development)

            var restSmart = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });

            var reqSmartPin = new RestRequest
            {
                Method = WebRequestMethods.Http.Post,
                PageUrl = "",//ConfigAuth.PageSmartDevices,
                Host = ConfigAuth.Host,
                BodyJsonString = JsonSerializer.SerializeJson(new SmartPin {UserPin = authDeviceGetTokenOut.UserCode})
            };

            reqDeviceGetToken.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
            var response = restSmart.RestPut(reqSmartPin);
            Assert.IsTrue(response.Length > 0);
            // No need to validate response. Call needed to set PIN


            // AUTH: Obtain Access Token
            var reqObtainAccessToken = new RestRequest
            {
                Host = ConfigAuth.Host
            };

            reqObtainAccessToken.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            reqObtainAccessToken.BodyParameter.Add(AuthConst.Client_Id, ConfigAuth.ClientCredentials.ClientId);
            reqObtainAccessToken.BodyParameter.Add(AuthConst.Client_Secret, ConfigAuth.ClientCredentials.ClientSecret);
            reqObtainAccessToken.BodyParameter.Add(AuthConst.Code, authDeviceGetTokenOut.DeviceCode);

            var authDeviceTokenOut = rest.RestPost<Token>(reqObtainAccessToken);

            Assert.AreEqual(authDeviceTokenOut.TokenType, TokenTypeBearer);
            Assert.AreEqual(authDeviceTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceTokenOut.RefreshToken.Length, 40);


            // Refresh Token
            var reqRefreshExpiredToken = new RestRequest
            {
                Host = ConfigAuth.Host
            };

            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.RrefreshToken);
            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.Client_Id, ConfigAuth.ClientCredentials.ClientId);
            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.Client_Secret, ConfigAuth.ClientCredentials.ClientSecret);
            reqRefreshExpiredToken.BodyParameter.Add(AuthConst.RefreshToken, authDeviceTokenOut.RefreshToken);

            var authRefreshTokenOut = rest.RestPost<Token>(reqRefreshExpiredToken);

            Assert.AreEqual(authRefreshTokenOut.AccessToken.Length, 26);
            Assert.AreEqual(authRefreshTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authRefreshTokenOut.TokenType, TokenTypeBearer);

            Debug.WriteLine(authRefreshTokenOut.AccessToken);

            ConfigStomp.Login = authRefreshTokenOut.AccessToken;
            ConfigStomp.Password = authRefreshTokenOut.AccessToken;

            using (var client = new StompClient(ConfigStomp))
            {
                var connect = client.Connect();
                Assert.IsTrue(connect);
                Assert.AreEqual(client.StompClientStatus, EnumStompCoreStatus.Connected);

                var framePing = new StompFrame(StompCommands.SEND);
                framePing.Headers.Add(StompHeader.UserId, ConfigStomp.Login);
                framePing.Headers.Add(StompHeader.Destination, "/exchange/connect.api/ping");
                framePing.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                framePing.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");

                framePing.Body = "Testdaten";

                StompFrame frameIn = null;
                client.StompClientFrameArrived += (sender, args) => { frameIn = args.Frame; };
                client.SendFrame(framePing);

                // waiting for frame to come
                while (frameIn == null)
                {
                }

                Assert.IsTrue(frameIn.Body.Contains("Testdaten"));

                // check out heartbeat in trace
                Thread.Sleep(6000);

                client.Disconnect();
                Thread.Sleep(3000); // Wait for Disconnect Receipt to arrive
                Assert.IsTrue(client.StompClientStatus == EnumStompCoreStatus.Disconnected);
            }
        }

        [TestMethod, TestCategory("Auth")]
        public void Test_RestAuth_Until_AccessToken()
        {
            var rest = new RestAuth(ConfigAuth);

            var authDeviceGetTokenOut = rest.GetDeviceAuthCode();
            Assert.AreEqual(authDeviceGetTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceGetTokenOut.Interval, 5);
            Assert.AreEqual(authDeviceGetTokenOut.VerificationUrl, VerificationUrl);


            // Set pin via SMART REST (only development)
            var reqSmartPin = new RestRequest
            {
                PageUrl = "",//ConfigAuth.PageSmartDevices,
                Host = ConfigAuth.Host,
                BodyJsonString = JsonSerializer.SerializeJson(new SmartPin {UserPin = authDeviceGetTokenOut.UserCode})
            };

            reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
            var restSmart = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });

            var response = restSmart.RestPut(reqSmartPin);
            Assert.IsTrue(response.Length > 0);
            // No need to validate response. Call needed to set PIN

            var authDeviceTokenOut = rest.ObtainAuthToken(authDeviceGetTokenOut.DeviceCode);
            Assert.AreEqual(authDeviceTokenOut.TokenType, TokenTypeBearer);
            Assert.AreEqual(authDeviceTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceTokenOut.RefreshToken.Length, 40);

            var authRefreshTokenOut = rest.RefreshToken(authDeviceTokenOut.RefreshToken);
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