namespace Secucard.Connect.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp.Client;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Base
    {
        protected const string logPath = "data\\secucard.cliend.log";
        protected const string storagePath = "data\\secucard.sec";
        protected const string fullTracePath = @"d:\trace\secucard\secucard.log";
        protected const string fullStoragePath = @"d:\trace\secucard\storage.sec";
        protected const string host = "core-dev10.secupay-ag.de";

        protected readonly ClientConfiguration Config;
        protected readonly StompConfig StompConfig;
        protected readonly RestConfig RestConfig;
        protected AuthConfig AuthConfig;

        protected MemoryDataStorage Storage;
        protected SecucardTraceFile Tracer;
        protected const string configPath = "data\\Config\\SecucardConnect.config";
        protected readonly Properties properties;

        protected bool IsConnected;
        protected bool MessageReceived;

        protected Test_Base()
        {
            properties = Properties.Read(configPath);

            // Setting current Access Token
            properties["Stomp.Password"] = "i0tdt3fgv31vde64kp49db3827";
            properties["Stomp.Login"] = "i0tdt3fgv31vde64kp49db3827";

            Config = new ClientConfiguration(properties);
            StompConfig = new StompConfig(properties);
            RestConfig = new RestConfig(properties);
            AuthConfig = new AuthConfig(properties);

            //{
            //    Host = host,
            //    Port = 61614,
            //    AcceptVersion = "1.2",
            //    HeartbeatClientMs = 5000,
            //    HeartbeatServerMs = 5000,
            //    Ssl = true
            //};
        }
    }
}