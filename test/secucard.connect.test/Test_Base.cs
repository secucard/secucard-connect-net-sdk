namespace Secucard.Connect.Test
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Stomp;

    public class Test_Base
    {
        protected const string logPath = "data\\secucard.cliend.log";
        protected const string storagePath = "data\\secucard.sec";

        protected readonly SecucardTraceFile Tracer;
        protected readonly MemoryDataStorage Storage;

        protected const string fullTracePath = @"d:\trace\secucard\secucard.log";
        protected const string fullStoragePath = @"d:\trace\secucard\storage.sec";
        protected readonly AuthConfig ConfigAuth;
        protected readonly StompConfig ConfigStomp;

        protected Test_Base()
        {
            const string host = "connect.secucard.com";
            //const string host = "core-dev10.secupay-ag.de"; // 91.195.151.211
            //        AuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/",

            ConfigStomp = new StompConfig
            {
                Host = host,
                VirtualHost = host,
                Port = 61614,
                AcceptVersion = "1.2",
                HeartbeatClientMs = 5000,
                HeartbeatServerMs = 5000,
                UseSsl = true
            };

            ConfigAuth = new AuthConfig
            {
                Host = host,
                AuthType = AuthTypeEnum.Device,
                AuthUrl = "https://connect.secucard.com/",
                AuthWaitTimeoutSec = 240,
                ClientId = "611c00ec6b2be6c77c2338774f50040b",
                Secret = "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb",
                Uuid = "/vendor/unknown/cashier/dotnettest1",
                PageOauthToken = "oauth/token",
                PageSmartDevices = "api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin",
                ClientCredentials =
                    new ClientCredentials("611c00ec6b2be6c77c2338774f50040b",
                        "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb")
            };

            Tracer = new SecucardTraceFile(logPath);
            Storage = MemoryDataStorage.LoadFromFile(storagePath);

        }
    }
}