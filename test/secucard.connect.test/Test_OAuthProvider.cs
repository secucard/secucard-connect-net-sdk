namespace Secucard.Connect.Test
{
    using System;
    using System.IO;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using secucard.connect;
    using Secucard.Connect.auth;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Model;
    using Secucard.Model.Smart;

    [TestClass]
    public class Test_OAuthProvider : Test_Base
    {
        [TestMethod, TestCategory("OAuthProvider")]
        [DeploymentItem("data", "data")]
        public void Test_OAuthDevice()
        {
            const string logPath = "data\\secucard.cliend.log";
            const string storagePath = "data\\secucard.cliend.sec";

            // Delete storage and log
            File.Delete(storagePath);
            File.Delete(logPath);

            var tracer = new SecucardTraceFile(logPath);
            var storage = MemoryDataStorage.LoadFromFile("data\\storage.sec");
            storage.Clear();
            // first run with empty storage
            var authProvider = new AuthProvider("testprovider", ConfigAuth, tracer, storage);
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            var token = authProvider.GetToken(true);
            Assert.IsNotNull(token.AccessToken);
            storage.SaveToFile(fullStoragePath);

            // second run with token in storage still valid
            authProvider = new AuthProvider("testprovider", ConfigAuth, tracer, storage);
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            token = authProvider.GetToken(true);
            Assert.IsNotNull(token.AccessToken);
            storage.SaveToFile(fullStoragePath);

            // third run wird token exired, needs refresh
            // second run with token in storage still valid
            token.ExpireTime = DateTime.Now.AddMinutes(-1);
            storage.Save("token-" + "testprovider", token);
            authProvider = new AuthProvider("testprovider", ConfigAuth, tracer, storage);
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            token = authProvider.GetToken(true);
            Assert.IsNotNull(token.AccessToken);
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
                    PageUrl = ConfigAuth.PageSmartDevices,
                    Host = ConfigAuth.Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin {UserPin = args.DeviceAuthCodes.UserCode})
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var rest = new RestAuth(ConfigAuth);
                var response = rest.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}