/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Test.Rest
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Payment.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Payment_Constracts : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Contracts_1_GET()
        {
            var request = new RestRequest
            {
                Token = Token,
                QueryParams = new QueryParams
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
                Token = Token,
                QueryParams = new QueryParams
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
                Token = Token,
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