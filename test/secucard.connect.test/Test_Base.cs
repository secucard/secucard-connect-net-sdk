namespace secucard.connect.test
{
    using secucard.connect.Auth;
    using secucard.stomp;

    public class Test_Base
    {
        protected readonly StompConfig ConfigStomp;
        protected AuthConfig ConfigAuth;

        protected Test_Base()
        {
            const string host = "dev10.secupay-ag.de"; // 91.195.151.211

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
                AuthUrl = "https://core-dev10.secupay-ag.de/app.core.connector/",
                ClientId = "611c00ec6b2be6c77c2338774f50040b",
                Secret = "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb",
                Uuid = "/vendor/unknown/cashier/dotnettest1",
                PageOauthToken = "oauth/token",
                PageSmartDevices = "api/v2/Smart/Devices/SDV_2FUFB3YJQ2YBHEDJKBSA9Q57NM8UA6/pin"
            };



        }
    }
}