namespace Secucard.Connect.Test
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

        protected SecucardTraceFile Tracer;
        protected MemoryDataStorage Storage;

        protected ClientConfiguration ClientConfiguration;

        protected Test_Client_Base()
        {
            ClientConfiguration = new ClientConfiguration
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
                                       BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/"
                                   }

           };


        }
    }
}