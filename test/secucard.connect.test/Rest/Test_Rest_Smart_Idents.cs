namespace secucard.connect.test.Rest
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.rest;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Smart_Idents : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Smart_Idents_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "Smart/Idents",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<Ident>(request);

            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_Smart_Idents_Validate_POST()
        {
            var request = new RestRequest
            {
                Token = Token,
                Objects = new List<SecuObject> { new Ident { Value = "9276123456789012", Type = "card" } },
                PageUrl = "Smart/Idents/notused/validate",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.PostObjectList<Ident>(request);

            Assert.AreEqual(data.First().Id, "smi_1");
        }

    }
}