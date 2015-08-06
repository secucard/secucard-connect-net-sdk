namespace Secucard.Connect.Test
{
    using System;
    using System.IO;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using secucard.connect.test.Rest;
    using Secucard.Connect.auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    [TestClass]
    public class Test_OAuthProvider : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("OAuthProvider")]
        [DeploymentItem("data", "data")]
        public void Test_OAuthDevice()
        {
            const string logPath = "data\\secucard.client.log";
            const string storagePath = "data\\secucard.client.sec";

            // Delete storage and log
            File.Delete(storagePath);
            File.Delete(logPath);

            RestAuth restAuth = new RestAuth(ConfigAuth);

            var tracer = new SecucardTraceFile(logPath);
            var storage = MemoryDataStorage.LoadFromFile("data\\storage.sec");
            storage.Clear();
            // first run with empty storage
            var tokenManager = new TokenManager(ConfigAuth, ClientAuthDetails, restAuth)
            {
                Context = new ClientContext {SecucardTrace = Tracer}
            };

            tokenManager.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            var token = tokenManager.GetToken(true);
            Assert.IsNotNull(token);
            storage.SaveToFile(fullStoragePath);

            // second run with token in storage still valid
            tokenManager = new TokenManager(ConfigAuth, ClientAuthDetails, restAuth)
            {
                Context = new ClientContext {SecucardTrace = Tracer}
            };
            tokenManager.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            token = tokenManager.GetToken(true);
            Assert.IsNotNull(token);
            storage.SaveToFile(fullStoragePath);

            // third run wird token exired, needs refresh
            // second run with token in storage still valid
            //token.ExpireTime = DateTime.Now.AddMinutes(-1);
            storage.Save("token-" + "testprovider", token);
            tokenManager = new TokenManager(ConfigAuth, ClientAuthDetails, restAuth)
            {
                Context = new ClientContext {SecucardTrace = Tracer}
            };
            tokenManager.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            token = tokenManager.GetToken(true);
            Assert.IsNotNull(token);
            storage.SaveToFile(fullStoragePath);

        }

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthProviderStatusUpdateEventArgs args)
        {
            if (args.Status == AuthProviderStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Method = WebRequestMethods.Http.Post,
                    Host = ConfigAuth.Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin {UserPin = args.DeviceAuthCodes.UserCode})
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}