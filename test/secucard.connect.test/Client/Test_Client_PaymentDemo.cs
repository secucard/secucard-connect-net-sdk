﻿namespace secucard.connect.test.Client
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Test.Client;
    using Secucard.Connect.Trace;
    using Secucard.Model.General;
    using Secucard.Model.Payment;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_PaymentDemo : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_PaymentDemo_1()
        {
            var client = SecucardConnect.Create("id", ClientConfigurationUser, Storage, Tracer);
            client.SecucardConnectEvent += ClientOnSecucardConnectEvent;
            client.Connect();

            var customerService = client.GetService<CustomerService>();
            var containerService = client.GetService<ContainerService>();
            var debitService = client.GetService<DebitService>();
            var contractService =client.GetService<ContractService>();

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
            customer = customerService.CreateCustomer(customer);


            Container container = new Container
            {
                Type = Container.TYPE_BANK_ACCOUNT,
                PrivateData = new Data { Owner = "John Doe", Iban = "DE12500105170648489890", Bic = "INGDDEFFXXX" }
            };

            // create container and get back filled up
            container = containerService.CreateContainer(container);



                  // clone contract either mine or another contract when allowed
            CloneParams cloneParams = new CloneParams
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

            Contract contract = contractService.CloneMyContract(cloneParams);
            // or
            contract = contractService.CloneContract("contract-id", cloneParams);


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
            debit = debitService.CreateTransaction(debit);


            Console.WriteLine((Tracer as SecucardTraceMemory).GetAllTrace());
        }

        private void ClientOnSecucardConnectEvent(object sender, SecucardConnectEventArgs args)
        {
            Tracer.Info("ClientOnSecucardConnectEvent Status={0}", args.Status);
        }


    }
}