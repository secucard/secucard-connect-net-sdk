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
namespace Secucard.Connect.Test.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Smart;
    using Secucard.Connect.Product.Smart.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_SmartTransaction : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_SmartTransaction_1()
        {
            StartupClientDevice();

            var transactionService = Client.GetService<SmartTransactionsService>();
            var identService = Client.GetService<SmartIdentsService>();

            // select an ident
            var availableIdents = identService.GetList(null);
            if (availableIdents == null || availableIdents.Count == 0)
            {
                throw new Exception("No idents found.");
            }
            var ident = availableIdents.List.First(o => o.Id == "smi_1");
            ident.Value = "pdo28hdal";

            var selectedIdents = new List<Ident> {ident};

            var groups = new List<ProductGroup>
            {
                new ProductGroup {Id = "group1", Desc = "beverages", Level = 1}
            };

            var basket = new Basket();
            basket.AddProduct(new Product
            {
                Id = 1,
                ArticleNumber = "3378",
                Ean = "5060215249804",
                Desc = "desc1",
                Quantity = 5m,
                PriceOne = 1999,
                Tax = 7,
                Groups = groups
            });
            basket.AddProduct(new Product
            {
                Id = 2,
                ArticleNumber = "art2",
                Ean = "5060215249805",
                Desc = "desc2",
                Quantity = 1m,
                PriceOne = 999,
                Tax = 19,
                Groups = groups
            });
            basket.AddProduct(new Text {Id = 1, ParentId = 2, Desc = "text1"});
            basket.AddProduct(new Text {Id = 2, ParentId = 2, Desc = "text2"});
            basket.AddProduct(new Product
            {
                Id = 3,
                ArticleNumber = "08070",
                Ean = "60215249807",
                Desc = "desc3",
                Quantity = 2m,
                PriceOne = 219,
                Tax = 7,
                Groups = null
            });

            var basketInfo = new BasketInfo {Sum = 1, Currency = "EUR"};

            var newTrans = new Transaction
            {
                BasketInfo = basketInfo,
                Basket = basket,
                Idents = selectedIdents,
                MerchantRef = "merchant21",
                TransactionRef = "transaction99"
            };

            var transaction = transactionService.Create(newTrans);
            Assert.AreEqual(transaction.Status, Transaction.STATUS_CREATED);


            // you may edit some transaction data and update
            //newTrans.MerchantRef = "merchant";
            //transaction.TransactionRef = "trans1";
            //transaction = transactionService.Update(transaction);

            var type = "demo"; // demo|auto|cash
            // demo instructs the server to simulate a different (random) transaction for each invocation of startTransaction

            // Start Transaction
            var result = transactionService.Start(transaction.Id, type);

            var b = transactionService.Cancel(transaction.Id);
            Assert.IsTrue(b);
        }
    }
}