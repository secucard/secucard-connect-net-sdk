namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.Payment;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Payment_Containers : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Containers_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "Payment/Containers",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<Container>(request);

            Assert.IsTrue(data.Count > 0);
        }
    }
}