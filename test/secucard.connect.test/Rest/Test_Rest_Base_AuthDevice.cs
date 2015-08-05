namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect;
    using Secucard.Connect.auth;
    using Secucard.Connect.auth.Model;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Test;
    using Secucard.Connect.Trace;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Base_AuthDevice : Test_Base
    {
        protected readonly Token Token;
        protected readonly RestService RestService;
        protected IClientAuthDetails clientAuthDetails;

        

        public  Test_Rest_Base_AuthDevice()
        {
            ConfigAuth = new AuthConfig
            {
                Host = host,
                AuthType = AuthTypeEnum.Device,
                OAuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/oauth/token",
                WaitTimeoutSec = 240,
                Uuid = "/vendor/unknown/cashier/dotnettest1",
                ClientCredentials =
                    new ClientCredentials("611c00ec6b2be6c77c2338774f50040b",
                        "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb")
            };

            clientAuthDetails = new ClientAuthDetailsDeviceTest();


            Tracer = new SecucardTraceFile(logPath);
            Storage = MemoryDataStorage.LoadFromFile(storagePath);


            var authProvider = new TokenManager(ConfigAuth, clientAuthDetails, new RestAuth(ConfigAuth));
            authProvider.Context = new ClientContext() {SecucardTrace = Tracer};
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Token = authProvider.GetToken(true);
            //Storage.SaveToFile(storagePath); // Save new token 

            RestService = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/" });

        }

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthProviderStatusUpdateEventArgs args)
        {
            if (args.Status == AuthProviderStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = ConfigAuth.Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin { UserPin = args.DeviceAuthCodes.UserCode })
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}