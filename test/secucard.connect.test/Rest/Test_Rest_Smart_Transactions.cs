namespace Secucard.Connect.Test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Rest;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Smart_Transactions : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Smart_Transactions_1_GET()
        {
            var request = new RestRequest
            {
                Token = Token,
                QueyParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0
                },
                PageUrl = "Smart/Transactions",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetList<Transaction>(request);

            Assert.IsTrue(data.Count > 0);
        }
    }
}