namespace Secucard.Connect.Test
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using secucard.connect;
    using Secucard.Connect.auth;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Model;
    using Secucard.Model.General;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General : Test_Base
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_1()
        {
            const string logPath = "data\\secucard.cliend.log";
            const string storagePath = "data\\secucard.sec";

            var tracer = new SecucardTraceFile(logPath);
            var storage = MemoryDataStorage.LoadFromFile(storagePath);

            var authProvider = new AuthProvider("testprovider", ConfigAuth, tracer, storage);
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            var token = authProvider.GetToken(true);
            storage.SaveToFile(storagePath);

            var restService = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/" });
            var request = new RestRequest
            {
                Token = token.AccessToken,
                QueyParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0
                    //SortOrder = new NameValueCollection { { "sort[a]", "desc" } }
                    //Fields = new List<string> { "a", "b" }
                },
                PageUrl = "General/Skeletons",//new Skeleton().ServiceResourceName.Replace(".", "/"),
                Host = "core-dev10.secupay-ag.de"
            };

            var list = restService.Get<ObjectList<Skeleton>>(request);

            Assert.IsTrue(list.Count > 0);
        }

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthProviderStatusUpdateEventArgs args)
        {
            if (args.Status == AuthProviderStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    PageUrl = ConfigAuth.PageSmartDevices,
                    Host = ConfigAuth.Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin { UserPin = args.DeviceAuthCodes.UserCode })
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var rest = new RestAuth(ConfigAuth);
                var response = rest.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}