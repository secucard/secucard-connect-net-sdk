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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment;
    using Secucard.Connect.Product.Payment.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_PaymentDemo : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_PaymentDemo_1()
        {
            StartupClientUser();


            var customerService = Client.GetService<CustomerPaymentService>();
            var containerService = Client.GetService<ContainersService>();
            var debitService = Client.GetService<SecupayDebitsService>();
            var contractService = Client.GetService<ContractService>();

            var customer = new Customer
            {
                Contact = new Contact
                {
                    Forename = "forename",
                    Surname = "surname",
                    Address = new Address
                    {
                        City = "city",
                        Street = "street"
                    }
                }
                // set more ...
            };

            // create customer and get back filled up
            customer = customerService.Create(customer);


            var container = new Container
            {
                Type = Container.TYPE_BANK_ACCOUNT,
                PrivateData = new Data {Owner = "John Doe", Iban = "DE12500105170648489890", Bic = "INGDDEFFXXX"}
            };

            // create container and get back filled up
            container = containerService.Create(container);


            // clone contract either mine or another contract when allowed
            var cloneParams = new CloneParams
            {
                Project = "project",
                AllowTransactions = true,
                PushUrl = "url",
                PaymentData = new Data
                {
                    Owner = "John Doe",
                    Iban = "DE12500105170648489890",
                    Bic = "INGDDEFFXXX"
                }
            };

            //return;
            // TODO: below with errors.

            //Contract contract = contractService.CloneMyContract(cloneParams);
            // or
            //Contract contract = contractService.Clone("contract-id", cloneParams);


            SecupayDebit debit = new SecupayDebit
            {
                Container = container,
                Customer = customer,
                Amount = 1,
                Currency = "EUR",
                OrderId = "order1",
                Purpose = "food"
            };

            // pay, create transaction
            // Exception: api Key for payment does not allow debit payments.
            //var debitret = debitService.Create(debit);
        }
    }
}