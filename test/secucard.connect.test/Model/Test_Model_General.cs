namespace Secucard.Connect.Test
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Model;
    using Secucard.Model.General;
    using Secucard.Model.Payment;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_General : Test_Base
    {

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_0_doc()
        {
            string json = File.ReadAllText("Data\\Model\\General.Accounts.0.doc.json");
            var data_accounts = JsonSerializer.DeserializeJson<JsonEnvelope<List<Account>>>(json);
            Assert.AreEqual(data_accounts.Data.Count, 1);
            Assert.AreEqual(data_accounts.Data.First().Username, "j.doe");
            Assert.AreEqual(data_accounts.Data.First().Assignment.First().IsOwner, true);
            Assert.AreEqual(data_accounts.Data.First().Assignment.First().Type, "assignment");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_1()
        {
            string json = File.ReadAllText("Data\\Model\\General.Accounts.1.json");
            var account = JsonSerializer.DeserializeJson<Account>(json);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_2()
        {
            string json = File.ReadAllText("Data\\Model\\General.Accounts.2.json");
            var data_accounts = JsonSerializer.DeserializeJson<JsonEnvelope<List<Account>>>(json);
        }




        [TestMethod, TestCategory("Model")]
        public void Test_General_Contact_1()
        {
            string json = File.ReadAllText("Data\\Model\\General.Contacts.1.json");
            var contact = JsonSerializer.DeserializeJson<Contact>(json);
            Assert.AreEqual(contact.Forename, "Dummy");
            Assert.AreEqual(contact.Surname, "Merchant1");
            Assert.AreEqual(contact.Name, "Dummy Merchant1");
            Assert.AreEqual(contact.Salutation, "Herr");
            Assert.AreEqual(contact.Id, "CNT_2V8EC9RTT2Y772BFB5GQGNH6NM8UA6");
        }


        [TestMethod, TestCategory("Model")]
        public void Test_Payment_Container_0_doc()
        {
            var json = File.ReadAllText("Data\\Model\\Payment.Containers.0.doc.json");
            var data_container = JsonSerializer.DeserializeJson<JsonEnvelope<List<Container>>>(json);
            Assert.AreEqual(data_container.Data.Count, 1);
            var paymentContainer = data_container.Data.First();
            Assert.AreEqual(paymentContainer.Id, "pct_abc123");
            Assert.AreEqual(paymentContainer.Merchant.Id, "mrc_abc123");
            Assert.AreEqual(paymentContainer.PrivateData.Owner, "John Doe");
            Assert.AreEqual(paymentContainer.PublicData.Owner, "John Doe");
            Assert.AreEqual(paymentContainer.Type, "bank_account");
            Assert.AreEqual(paymentContainer.FormattedCreated, "2015-02-03T14:22:19+01:00");
        }




    }
}