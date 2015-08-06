namespace Secucard.Connect.Test.Client
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment;
    using Secucard.Connect.Product.Payment.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_CustomerService : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_CustomerService_1()
        {
            StartupClientUser();

            var customerService = Client.GetService<CustomerService>();

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

            var customerPost = customerService.Create(customer);

            var customerGet = customerService.GetList(new QueryParams {Query = "id:" + customerPost.Id}).List.First();

            customerGet.Contact.Forename = "ChangedForename-" + DateTime.Now.Ticks;
            var customerUpdate = customerService.Update(customerGet);

            var customerGetUpdate =
                customerService.GetList(new QueryParams {Query = "id:" + customerPost.Id}).List.First();
            Assert.AreEqual(customerGetUpdate.Contact.Forename, customerGet.Contact.Forename);
            customerService.Delete<Customer>(customerGetUpdate.Id);

            Thread.Sleep(1000);

            var customerGetWithout = customerService.GetList(new QueryParams {Query = "id:" + customerPost.Id});
            Assert.AreEqual(customerGetWithout.Count, 0);
        }
    }
}