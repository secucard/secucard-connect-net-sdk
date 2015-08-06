namespace Secucard.Connect.Test.Config
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Rest;
    using Secucard.Stomp;

    [TestClass]
    public class Test_Config
    {
        [TestMethod]
        [TestCategory("Config")]
        [DeploymentItem("Data//Config", "Data//Config")]
        public void Test_Config_1()
        {
            const string configPath = "Data//Config//SecucardConnect.config";
            if (File.Exists(configPath)) File.Delete(configPath);

            var config = new ClientConfiguration
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
                    BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/"
                }
            };


            config.Save(configPath);

            var config2 = ClientConfiguration.Load(configPath);

            Assert.AreEqual(config2.DefaultChannel, config.DefaultChannel);
            Assert.AreEqual(config2.AndroidMode, config.AndroidMode);
            Assert.AreEqual(config2.CacheDir, config.CacheDir);
            Assert.AreEqual(config2.DeviceId, config.DeviceId);
        }
    }
}