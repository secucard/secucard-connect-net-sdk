namespace Secucard.Connect.Test
{
    using Secucard.Connect.auth;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Stomp;

    public class Test_Base
    {
        protected const string logPath = "data\\secucard.cliend.log";
        protected const string storagePath = "data\\secucard.sec";

        protected SecucardTraceFile Tracer;
        protected MemoryDataStorage Storage;

        protected const string fullTracePath = @"d:\trace\secucard\secucard.log";
        protected const string fullStoragePath = @"d:\trace\secucard\storage.sec";

        protected AuthConfig ConfigAuth;
        protected readonly StompConfig ConfigStomp;
        protected const string host = "core-dev10.secupay-ag.de";

        protected Test_Base()
        {
            ConfigStomp = new StompConfig
            {
                Host = host,
                Port = 61614,
                AcceptVersion = "1.2",
                HeartbeatClientMs = 5000,
                HeartbeatServerMs = 5000,
                Ssl = true
            };


        }
    }
}