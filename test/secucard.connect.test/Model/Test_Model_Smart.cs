namespace Secucard.Connect.Test
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Model;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_Smart
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Smart_Idents_1()
        {
            var json = File.ReadAllText("Data\\Model\\Smart.Idents.1.json");
            var data = JsonSerializer.DeserializeJson<JsonEnvelope<List<Ident>>>(json);
            Assert.AreEqual(data.Data.Count, 3);
            var smartIdent = data.Data.First();
            Assert.AreEqual(smartIdent.Id, "smi_1");
            Assert.AreEqual(smartIdent.Type, "card");
            Assert.AreEqual(smartIdent.Prefix, "9276");
            Assert.AreEqual(smartIdent.Length, 16);
            Assert.AreEqual(smartIdent.Name, "secucard Kundenkarte");
        }


        [TestMethod, TestCategory("Model")]
        public void Test_Smart_Transactions_1()
        {
            var json = File.ReadAllText("Data\\Model\\Smart.Transactions.1.json");
            var data = JsonSerializer.DeserializeJson<JsonEnvelope<List<Transaction>>>(json);
            Assert.IsTrue(data.Data.Count> 1);
            var transaction = data.Data.First();
            Assert.AreEqual(transaction.Id, "STX_TC6Z542ZT2Y7US4VR5GQG8DMHF7FA0");
            Assert.AreEqual(transaction.Status, "failed");
            Assert.AreEqual(transaction.TransactionRef, "Beleg4536676");
            Assert.AreEqual(transaction.MerchantRef, "Beleg4536676");
            Assert.IsNotNull(transaction.Basket);
            Assert.AreEqual(transaction.FormattedCreated, "2015-03-10T13:19:11+01:00");
            Assert.AreEqual(transaction.FormattedUpdated, "2015-03-10T13:19:40+01:00");
            var product1 = data.Data.First().Basket.Products.First();
            Assert.AreEqual(product1.ArticleNumber, "70000");

        }
    }
}