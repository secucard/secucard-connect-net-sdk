namespace Secucard.Connect.Test.Client
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_Resource : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralStores_Resource()
        {
            StartupClientDevice();
            var store = Client.General.Stores.Get("STO_3SGKT879YSRC27NEJSG6BJ85P4CKP8");
            Assert.IsNotNull(store);

            var bytes= store.Logo.GetContents();
            Assert.IsNotNull(bytes);
            Assert.AreEqual(bytes.Length, 11247);
        }

    }
}