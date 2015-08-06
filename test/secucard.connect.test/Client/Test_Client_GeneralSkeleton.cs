namespace Secucard.Connect.Test.Client
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Trace;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_GeneralSkeleton : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeleton_1()
        {
            StartupClientUser();

            var skeletonService = Client.GetService<GeneralSkeletonsService>();

            // select an ident
            var skeletons = skeletonService.GetList(null);
        }


    }
}