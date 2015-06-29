namespace secucard.connect.test.Rest
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.General;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Deliveryadresses : Test_Rest_BaseGeneral
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Deliveryaddresses_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token.AccessToken,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "General/Deliveryaddresses",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<DeliveryAddress>(request);

            Assert.IsTrue(data.Count > 0);
        }


     

    }
}