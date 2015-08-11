namespace Secucard.Connect.Test.Config
{
    using System.Diagnostics;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Client.Config;

    [TestClass]
    public class Test_Config
    {
        [TestMethod]
        [TestCategory("Config")]
        [DeploymentItem("Data//Config", "Data//Config")]
        public void Test_Config_Properties()
        {
            const string configPath = "Data//Config//SecucardConnect.config";
            if (File.Exists(configPath)) File.Delete(configPath);

            Properties props = new Properties();
            props.Set("Appid", "ApplikationId");
            props.Set("test", "test");
            props.Write(configPath);

            var props2 = Properties.Read(configPath);
            Debug.Write(File.ReadAllText(configPath));

            Assert.AreEqual(props.Get("Appid"),props2.Get("Appid"));
        }
    }
}