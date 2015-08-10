namespace Secucard.Connect.Test.Model
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Loyalty.Model;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_Loyalty
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Loyalty_Cardgroups_1()
        {
            var json = File.ReadAllText("Data\\Model\\Loyalty.Cardgroups.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<CardGroup>>(json);
            Assert.IsTrue(data.List.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Loyalty_Cards_1()
        {
            var json = File.ReadAllText("Data\\Model\\Loyalty.Cards.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Card>>(json);
            Assert.IsTrue(data.List.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Loyalty_MerchantCards_1()
        {
            var json = File.ReadAllText("Data\\Model\\Loyalty.MerchantCards.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<MerchantCard>>(json);
            Assert.IsTrue(data.List.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Loyalty_Customers_1()
        {
            var json = File.ReadAllText("Data\\Model\\Loyalty.Customers.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Customer>>(json);
            Assert.IsTrue(data.List.Count > 0);
        }
    }
}