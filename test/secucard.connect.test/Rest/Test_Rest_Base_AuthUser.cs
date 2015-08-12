namespace Secucard.Connect.Test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Base_AuthUser : Test_Base
    {
        protected readonly RestService RestService;
        protected readonly string Token;

        public Test_Rest_Base_AuthUser()
        {
            properties["Auth.ExtendExpire"] = "true";

            AuthConfig = new AuthConfig(properties);

            //Storage = MemoryDataStorage.LoadFromFile("data\\secucard.payment.sec");
            IClientAuthDetails clientAuthDetails = new ClientAuthDetailsUserTest();

            var authProvider = new TokenManager(AuthConfig, clientAuthDetails, new RestAuth(AuthConfig));

            authProvider.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;
            Token = authProvider.GetToken(true);
            //Storage.SaveToFile("data\\secucard.payment.sec"); // Save new token 

            RestService =
                new RestService("https://core-dev10.secupay-ag.de/app.core.connector/api/v2/");
        }

        private void TokenManagerOnTokenManagerStatusUpdateEvent(object sender, TokenManagerStatusUpdateEventArgs args)
        {
            if (args.Status == AuthStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = AuthConfig.Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin {UserPin = args.DeviceAuthCodes.UserCode})
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart =
                    new RestService("https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin");
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}