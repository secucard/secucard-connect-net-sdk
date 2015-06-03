using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace secucard.connect.test
{
    using System.Diagnostics;
    using secucard.connect.auth;
    using secucard.connect.rest;
    using secucard.model;
    using secucard.model.auth;
    using secucard.model.smart;

    [TestClass]
    public class Test_RestAuth
    {
        #region ### Const ###

        // TODO: Will have to move to config files

        private const string AuthUrl = "https://connect.secucard.com/";
        //    private const string AuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/";

        private const string ApiUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/";
        // private const string ApiUrl = "https://connect.secucard.com/api/v2/";

        private const string Host = "core-dev10.secupay-ag.de";
        // private const string Host = "connect.secucard.com";

        private const string ClientId = "611c00ec6b2be6c77c2338774f50040b";
        private const string Secret = "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb";
        private const string Uuid = "/vendor/unknown/cashier/iostest1";
        //private const string PageOauthToken = "oauth/token";
        private const string PageOauthToken = "app.core.connector/oauth/token";

        private const string PageSmartDevices =
            "app.core.connector/api/v2/Smart/Devices/SDV_3E3S4XR332YASA3MB5GQGMW2R3YNAA/pin";

        private const string VerificationUrl = "http://www.secuoffice.com";
        private const string TokenTypeBearer = "bearer";


        #endregion

        [TestMethod, TestCategory("Auth")]
        public void Test_Auth_Device_Start()
        {
            var request = new RestRequest(AuthUrl)
            {
                PageUrl = PageOauthToken,
                Host = Host
            };

            request.Parameter.Add(AuthConst.Client_Id, ClientId);
            request.Parameter.Add(AuthConst.Client_Secret, Secret);
            request.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            request.Parameter.Add(AuthConst.Uuid, Uuid);

            RestAuth rest = new RestAuth();
            rest.RestPost(request);

        }

        [TestMethod, TestCategory("Auth")]
        public void Test_Auth_Until_AccessToken()
        {
            var rest = new RestAuth();

            // AUTH: GetToken
            var reqDeviceGetToken = new RestRequest(AuthUrl)
            {
                PageUrl = PageOauthToken,
                Host = Host
            };

            reqDeviceGetToken.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            reqDeviceGetToken.Parameter.Add(AuthConst.Client_Id, ClientId);
            reqDeviceGetToken.Parameter.Add(AuthConst.Client_Secret, Secret);
            reqDeviceGetToken.Parameter.Add(AuthConst.Uuid, Uuid);

            var authDeviceGetTokenOut = rest.RestPost<AuthCodeDevice>(reqDeviceGetToken);

            Assert.AreEqual(authDeviceGetTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceGetTokenOut.Interval, 5);
            Assert.AreEqual(authDeviceGetTokenOut.VerificationUrl, VerificationUrl);


            // Set pin via SMART REST (only development)

            var reqSmartPin = new RestRequest(AuthUrl)
            {
                PageUrl = PageSmartDevices,
                Host = Host,
                BodyJsonString = JsonSerializer.SerializeJson(new SmartPin { UserPin = authDeviceGetTokenOut.UserCode })
            };

            reqDeviceGetToken.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
            string response = rest.RestPut(reqSmartPin);
            Assert.IsTrue(response.Length > 0);
            // No need to validate response. Call needed to set PIN


            // AUTH: Obtain Access Token
            var reqObtainAccessToken = new RestRequest(AuthUrl)
            {
                PageUrl = PageOauthToken,
                Host = Host
            };

            reqObtainAccessToken.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.Device);
            reqObtainAccessToken.Parameter.Add(AuthConst.Client_Id, ClientId);
            reqObtainAccessToken.Parameter.Add(AuthConst.Client_Secret, Secret);
            reqObtainAccessToken.Parameter.Add(AuthConst.Code, authDeviceGetTokenOut.DeviceCode);

            var authDeviceTokenOut = rest.RestPost<AuthToken>(reqObtainAccessToken);

            Assert.AreEqual(authDeviceTokenOut.TokenType, TokenTypeBearer);
            Assert.AreEqual(authDeviceTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authDeviceTokenOut.RefreshToken.Length, 40);


            // Refresh Token
            var reqRefreshExpiredToken = new RestRequest(AuthUrl)
            {
                PageUrl = PageOauthToken,
                Host = Host
            };

            reqRefreshExpiredToken.Parameter.Add(AuthConst.Grant_Type, AuthGrantTypeConst.RrefreshToken);
            reqRefreshExpiredToken.Parameter.Add(AuthConst.Client_Id, ClientId);
            reqRefreshExpiredToken.Parameter.Add(AuthConst.Client_Secret, Secret);
            reqRefreshExpiredToken.Parameter.Add(AuthConst.RefreshToken, authDeviceTokenOut.RefreshToken);

            var authRefreshTokenOut = rest.RestPost<AuthToken>(reqRefreshExpiredToken);

            Assert.AreEqual(authRefreshTokenOut.AccessToken.Length, 26);
            Assert.AreEqual(authRefreshTokenOut.ExpiresIn, 1200);
            Assert.AreEqual(authRefreshTokenOut.TokenType, TokenTypeBearer);

            Debug.WriteLine(authRefreshTokenOut.AccessToken);
 
        }
    }
}
