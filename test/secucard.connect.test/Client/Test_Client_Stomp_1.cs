namespace Secucard.Connect.Test.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_Stomp_1 : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeletonStomp_1_GET()
        {
            StartupClientDevice();

            var service = Client.GetService<GeneralSkeltonsServiceStomp>();

            var queryParams = new QueryParams
            {
                Count = 2,
                ScrollExpire = "5m",
                SortOrder = new Dictionary<string, string>() { { "a", QueryParams.SORT_ASC } },
                Fields = new List<string> { "id", "a", "b" }
            };

            //TEST Heartbeat
            Thread.Sleep(20000);

            var list = service.GetList(queryParams);
            Assert.IsTrue(list.Count > 0);

            var skeleton = service.Get(list.List.First().Id);
            // ERROR: Assert.AreEqual(skeleton.A, "abc1");
        }
    }
}