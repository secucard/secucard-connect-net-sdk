namespace Secucard.Connect.Test.Client
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using secucard.connect.test;
    using Secucard.Connect.auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Stomp;

    public class Test_Client_Base
    {
        protected const string logPath = "data\\secucard.cliend.log";
        protected const string storagePath = "data\\secucard.sec";

        protected ISecucardTrace Tracer;
        protected MemoryDataStorage Storage;

        protected ClientConfiguration ClientConfigurationDevice;
        protected ClientConfiguration ClientConfigurationUser;

        protected Test_Client_Base()
        {
            Tracer = new SecucardTraceMemory();
            Storage = new MemoryDataStorage();


            ClientConfigurationDevice = new ClientConfiguration
               {
                   AndroidMode = false,
                   CacheDir = null,
                   DefaultChannel = "REST",
                   DeviceId = "",
                   StompEnabled = true,
                   HeartBeatSec = 30,
                   AuthConfig = new AuthConfig
                                       {
                                           Host = "core-dev10.secupay-ag.de",
                                           AuthType = AuthTypeEnum.Device,
                                           OAuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/oauth/token",
                                           AuthWaitTimeoutSec = 240,
                                           Uuid = "/vendor/unknown/cashier/dotnettest1"
                                       },
                   StompConfig = new StompConfig
                                       {
                                           Host = "dev10.secupay-ag.de",
                                           Port = 61614,
                                           Login = "v7ad2eejbgt135q6v47vehopg7",
                                           Password = "v7ad2eejbgt135q6v47vehopg7",
                                           AcceptVersion = "1.2",
                                           HeartbeatClientMs = 5000,
                                           HeartbeatServerMs = 5000,
                                           Ssl = true
                                       },
                   RestConfig = new RestConfig
                                       {
                                           Host = "core-dev10.secupay-ag.de",
                                           BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/"
                                       },
                   ClientAuthDetails = new ClientAuthDetailsDeviceTest(),
                   SecucardTrace = Tracer,
                   DataStorage = Storage
               };

            ClientConfigurationUser = new ClientConfiguration
            {
                AndroidMode = false,
                CacheDir = null,
                DefaultChannel = "REST",
                DeviceId = "",
                StompEnabled = true,
                HeartBeatSec = 30,
                AuthConfig = new AuthConfig
                {
                    Host = "core-dev10.secupay-ag.de",
                    AuthType = AuthTypeEnum.User,
                    OAuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/oauth/token",
                    AuthWaitTimeoutSec = 240,
                    Uuid = "/vendor/unknown/cashier/dotnettest1"
                },
                StompConfig = new StompConfig
                {
                    Host = "dev10.secupay-ag.de",
                    Port = 61614,
                    Login = "v7ad2eejbgt135q6v47vehopg7",
                    Password = "v7ad2eejbgt135q6v47vehopg7",
                    AcceptVersion = "1.2",
                    HeartbeatClientMs = 5000,
                    HeartbeatServerMs = 5000,
                    Ssl = true
                },
                RestConfig = new RestConfig
                {
                    Host = "core-dev10.secupay-ag.de",
                    BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/"
                },
                ClientAuthDetails = new ClientAuthDetailsUserTest(),
                SecucardTrace = Tracer,
                DataStorage = Storage
            };
        }

        /// <summary>
        /// Handles Device Authentification. Enter pin thru web interface service
        /// </summary>
        public void ClientOnSecucardConnectEvent(object sender, SecucardConnectEventArgs args)
        {
            Tracer.Info("ClientOnSecucardConnectEvent Status={0}", args.Status);

            if (args.Status == AuthProviderStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = ClientConfigurationDevice.AuthConfig.Host,
                    BodyJsonString = JsonSerializer.SerializeJson(new SmartPin { UserPin = args.DeviceAuthCodes.UserCode })
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }
    }
}