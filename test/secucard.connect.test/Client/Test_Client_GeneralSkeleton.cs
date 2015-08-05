namespace secucard.connect.test.Client
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Test.Client;
    using Secucard.Connect.Trace;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_GeneralSkeleton : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeleton_1()
        {
            var client = SecucardConnect.Create( ClientConfigurationUser);
            client.SecucardConnectEvent += ClientOnSecucardConnectEvent;
            client.Connect();

            var skeletonService = client.GetService<GeneralSkeletonsService>();

            // select an ident
            var skeletons = skeletonService.GetList(null);
            
            client.Disconnect();

            Console.WriteLine((Tracer as SecucardTraceMemory).GetAllTrace());
        }

    }
}