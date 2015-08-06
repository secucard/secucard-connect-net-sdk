namespace Secucard.Connect.Test
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Payment.Model;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_Payment : Test_Base
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Payment_Container_0_doc()
        {
            var json = File.ReadAllText("Data\\Model\\Payment.Containers.0.doc.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Container>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var paymentContainer = data.List.First();
            Assert.AreEqual(paymentContainer.Id, "pct_abc123");
            Assert.AreEqual(paymentContainer.Merchant.Id, "mrc_abc123");
            Assert.AreEqual(paymentContainer.PrivateData.Owner, "John Doe");
            Assert.AreEqual(paymentContainer.PublicData.Owner, "John Doe");
            Assert.AreEqual(paymentContainer.Type, "bank_account");
            Assert.AreEqual(paymentContainer.FormattedCreated, "2015-02-03T14:22:19+01:00");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Payment_Customers_0_doc()
        {
            var json = File.ReadAllText("Data\\Model\\Payment.Customers.0.doc.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Customer>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var obj = data.List.First();
            Assert.AreEqual(obj.Id, "PCU_3TGCQFGCR2Y8ZHPEB5GQGYPNRQUUAE");
            Assert.AreEqual(obj.Contact.Forename, "hans");
            Assert.AreEqual(obj.Contact.Surname, "surname");
            Assert.AreEqual(obj.Contact.CompanyName, "companyname");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Payment_Customers_1()
        {
            var json = File.ReadAllText("Data\\Model\\Payment.Customers.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Customer>>(json);
            Assert.AreEqual(data.List.Count, 10);
            var obj = data.List.First();
            Assert.AreEqual(obj.Id, "PCU_3TGCQFGCR2Y8ZHPEB5GQGYPNRQUUAE");
            Assert.AreEqual(obj.Contact.Forename, "hans");
            Assert.AreEqual(obj.Contact.Address.Street, "street");
            Assert.AreEqual(obj.Contract.Id, "PCR_3WYDQ6F7F2Y7GES9R5GQGGSMQN62A7");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Payment_Customers_2()
        {
            var json = File.ReadAllText("Data\\Model\\Payment.Customers.2.json");
            var data = JsonSerializer.DeserializeJson<List<Customer>>(json);
            //Assert.AreEqual(data.List.Count, 1);
            //var obj = data.List.First();
            //Assert.IsNotNull(obj);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Payment_Contracts_1()
        {
            var json = File.ReadAllText("Data\\Model\\Payment.Contracts.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Contract>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var obj = data.List.First();
            Assert.AreEqual(obj.Id, "PCR_3WYDQ6F7F2Y7GES9R5GQGGSMQN62A7");
            Assert.AreEqual(obj.Merchant.Id, "MRC_2CNA8BY26D2GJSPCXTXDD605KEK4PK");
            Assert.AreEqual(obj.AllowCloning, true);
            Assert.AreEqual(obj.InternalReference, "475094");
            Assert.AreEqual(obj.FormattedCreated, "2015-02-27T14:54:27+01:00");
        }
    }
}