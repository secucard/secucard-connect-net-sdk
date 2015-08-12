namespace Secucard.Connect.Test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Accounts : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Accounts_1_GET()
        {
            var request = new RestRequest
            {
                Token = Token,
                QueryParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0
                },
                PageUrl = "General/Accounts",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetList<Account>(request);

            Assert.IsTrue(data.Count > 0);
        }
    }
}