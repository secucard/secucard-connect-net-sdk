namespace Secucard.Connect.Test.Client
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.General;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_General : Test_Client_Base
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


        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralAccountDevices_Resource()
        {
            StartupClientDevice();
            var list = Client.General.Accountdevices.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralAccounts_Resource()
        {
            StartupClientDevice();
            var list = Client.General.Accounts.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralTransactions_Resource()
        {
            StartupClientDevice();
            var list = Client.General.GeneralTransactions.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralMerchants_Resource()
        {
            StartupClientDevice();
            var list = Client.General.Merchants.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralNews_Resource()
        {
            StartupClientDevice();
            var list = Client.General.News.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_PublicMerchants_Resource()
        {
            StartupClientDevice();
            var list = Client.General.Publicmerchants.GetList(null);
            Assert.IsNotNull(list);
        }


        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeleton_1()
        {
            StartupClientDevice();

            var skeletonService = Client.GetService<SkeletonsService>();

            var skeletons = skeletonService.GetList(null);

            skeletonService.CreateEvent();

            Thread.Sleep(10000);
        }
    }
}