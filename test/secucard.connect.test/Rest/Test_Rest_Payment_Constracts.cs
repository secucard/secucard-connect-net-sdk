namespace secucard.connect.test.Rest
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.Payment;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Payment_Constracts : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Contracts_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token.AccessToken,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "Payment/Contracts",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<Contract>(request);

            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Contracts_2_CLONE()
        {
            var request = new RestRequest
            {
                Token = Token.AccessToken,
                QueyParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0
                },
                PageUrl = "Payment/Contracts",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetList<Contract>(request);

            Assert.IsTrue(data.Count > 0);

            var requestClone = new RestRequest
            {
                Token = Token.AccessToken,
                Action = "clone",
                Id = data.List.First().Id,
                Object = new CloneParams
                {
                    AllowTransactions = true,
                    Project = "project_name",
                    PushUrl = "new_url_or_remove",
                    PaymentData = new Data
                    {
                        Owner = "John Doe",
                        Iban = "DE12500105170648489890",
                        Bic = "INGDDEFFXXX"
                    }
                },
                PageUrl = "Payment/Contracts",
                Host = "core-dev10.secupay-ag.de"
            };

            // var contract = RestService.Execute<Contract, CloneParams>(requestClone);


        }
    }
}