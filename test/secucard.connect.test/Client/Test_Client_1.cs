namespace Secucard.Connect.Test.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Trace;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_1 : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Service_General_Skeleton_1_GET()
        {
            var client = SecucardConnect.Create(ClientConfigurationDevice);
            client.AuthEvent += ClientOnAuthEvent;
            client.Connect();

            var service = client.GetService<GeneralSkeletonsService>();

            var queryParams = new QueryParams
            {
                Count = 10,
                ScrollExpire = "5m",
                SortOrder = new NameValueCollection {{"a", QueryParams.SORT_ASC}},
                Fields = new List<string> {"id", "a", "b"}
            };

            var list = service.GetList(queryParams);
            Assert.IsTrue(list.Count > 0);

            var skeleton = service.Get(list.List.First().Id);
            Assert.AreEqual(skeleton.A, "abc1");

            Console.WriteLine((Tracer as SecucardTraceMemory).GetAllTrace());
        }
    }
}