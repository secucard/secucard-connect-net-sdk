namespace Secucard.Connect.Test.Client
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
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
                                           WaitTimeoutSec = 240,
                                           Uuid = "/vendor/unknown/cashier/dotnettest1",
                                           ClientCredentials =
                                               new ClientCredentials("611c00ec6b2be6c77c2338774f50040b",
                                                   "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb")
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
                                       }

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
                    WaitTimeoutSec = 240,
                    Uuid = "/vendor/unknown/cashier/dotnettest1",
                    ClientCredentials =
                        new ClientCredentials("f0478f73afe218e8b5f751a07c978ecf",
                            "30644327cfbde722ad2ad12bb9c0a2f86a2bee0a2d8de8d862210112af3d01bb")
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
                }

            };



            Tracer = new SecucardTraceMemory();
            Storage = MemoryDataStorage.LoadFromFile(storagePath);
        }
    }
}