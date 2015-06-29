namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.auth;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Test;
    using Secucard.Connect.Trace;
    using Secucard.Model;
    using Secucard.Model.Auth;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Base_AuthUser : Test_Base
    {
        protected readonly AuthToken Token;
        protected readonly RestService RestService;

        public Test_Rest_Base_AuthUser()
        {
            ConfigAuth = new AuthConfig
            {
                Host = host,
                AuthType = AuthTypeEnum.User,
                AuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/",
                AuthWaitTimeoutSec = 240,
                Uuid = "/vendor/unknown/cashier/dotnettest1",
                PageOauthToken = "oauth/token",
                PageSmartDevices = "api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin",
                ClientCredentials =
                    new ClientCredentials("f0478f73afe218e8b5f751a07c978ecf",
                        "30644327cfbde722ad2ad12bb9c0a2f86a2bee0a2d8de8d862210112af3d01bb")
            };

            Tracer = new SecucardTraceFile(logPath);
            Storage = MemoryDataStorage.LoadFromFile("data\\secucard.payment.sec");


            var authProvider = new AuthProvider("testprovider", ConfigAuth, Tracer, Storage);
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Token = authProvider.GetToken(true);
            Storage.SaveToFile("data\\secucard.payment.sec"); // Save new token 

            RestService = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/" });

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