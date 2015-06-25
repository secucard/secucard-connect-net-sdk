namespace Secucard.Connect.Test
{
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Model;
    using Secucard.Model.Payment;
    using Customer = Secucard.Model.Loyalty.Customer;

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
            var data = JsonSerializer.DeserializeJson<ObjectList<Customer>>(json); // TODO: Customer???
            Assert.AreEqual(data.List.Count, 1);
            var obj = data.List.First();
            Assert.AreEqual(obj.Id, "pcu_abc123");
            Assert.AreEqual(obj.Merchant.Id, "mrc_abc123");
            Assert.AreEqual(obj.ForeName, "John");
            Assert.AreEqual(obj.SurName, "Doe");
            Assert.AreEqual(obj.Zipcode, "01234");
            Assert.AreEqual(obj.FormattedDateOfBirth, "1901-02-03T00:00:00+01:00");
        }




    }
}