namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using secucard.connect;
    using Secucard.Connect.auth;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Test;
    using Secucard.Model;
    using Secucard.Model.Auth;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_BaseGeneral : Test_Base
    {
        protected readonly AuthToken Token;
        protected readonly RestService RestService;

        public  Test_Rest_BaseGeneral()
        {
            var authProvider = new AuthProvider("testprovider", ConfigAuth, Tracer, Storage);
            authProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Token = authProvider.GetToken(true);
            Storage.SaveToFile(storagePath); // Save new token 

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