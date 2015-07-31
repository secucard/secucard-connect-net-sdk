namespace secucard.connect.test.Client
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Test.Client;
    using Secucard.Model;
    using Secucard.Model.General;
    using Secucard.Model.Payment;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_CustomerService : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_CustomerService_1()
        {
            var client = SecucardConnect.Create("id", ClientConfigurationUser, Storage, Tracer);
            client.SecucardConnectEvent += ClientOnSecucardConnectEvent;
            client.Connect();

            var customerService = client.GetService<CustomerService>();

            var customer = new Customer
            {
                Contact = new Contact
                {
                    Salutation = "Herr",
                    Title = "Dr.",
                    Forename = "Forename-" + DateTime.Now.Ticks,
                    Surname = "Surname-" + DateTime.Now.Ticks,
                    CompanyName = "Company-" + DateTime.Now.Ticks,
                    DateOfBirth = new DateTime(1970, 1, 1),
                    Email = "test@hutzlibu.com",
                    Phone = "0049-987-654321",
                    Mobile = "0049-170-654321",
                    Address = new Address
                    {
                        Street = "Hauptstrasse",
                        StreetNumber = "23a",
                        PostalCode = "01234",
                        City = "Entenhausen",
                        Country = "Germany"
                    }
                }
            };

            var customerPost = customerService.CreateCustomer(customer);

            var customerGet = customerService.GetCustomers(new QueryParams { Query = "id:" + customerPost.Id }).List.First();

            customerGet.Contact.Forename = "ChangedForename-" + DateTime.Now.Ticks;
            var customerUpdate = customerService.UpdateCustomer(customerGet);

            var customerGetUpdate = customerService.GetCustomers(new QueryParams { Query = "id:" + customerPost.Id }).List.First();
            Assert.AreEqual(customerGetUpdate.Contact.Forename, customerGet.Contact.Forename);
            customerService.DeleteCustomer(customerGetUpdate.Id);

            Thread.Sleep(1000);

            var customerGetWithout = customerService.GetCustomers(new QueryParams { Query = "id:" + customerPost.Id });
            Assert.AreEqual(customerGetWithout.Count,0);

        }



    }
}