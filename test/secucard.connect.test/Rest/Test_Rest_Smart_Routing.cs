namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Smart_Routing : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Smart_Routing_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token.AccessToken,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "Smart/Routings",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<Routing>(request);

            Assert.IsTrue(data.Count > 0);
        }
    }
}