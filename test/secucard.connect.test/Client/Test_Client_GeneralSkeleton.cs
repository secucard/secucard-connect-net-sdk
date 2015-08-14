namespace Secucard.Connect.Test.Client
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.General;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_GeneralSkeleton : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeleton_1()
        {
            StartupClientDevice();

            var skeletonService = Client.GetService<SkeletonsService>();

            // select an ident
            //var skeletons = skeletonService.GetList(null);


            skeletonService.CreateEvent();

            Thread.Sleep(10000);
        }
    }
}