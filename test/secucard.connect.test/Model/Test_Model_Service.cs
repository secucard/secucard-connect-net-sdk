namespace Secucard.Connect.Test
{
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Model;
    using Secucard.Model.Services;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_Service
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Service_Identrequest_1()
        {
            var json = File.ReadAllText("Data\\Model\\Services.Identrequest.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<IdentRequest>>(json);
            Assert.AreEqual(data.List.Count, 3);
            var identRequest = data.List.First();
            Assert.AreEqual(identRequest.Id, "SIR_24E46FTA92Y8GCHNR5GQG7PKRQUUAE");
            Assert.AreEqual(identRequest.Type, "person");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Service_Identresults_1()
        {
            var json = File.ReadAllText("Data\\Model\\Services.Identresults.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<IdentResult>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var identResult = data.List.First();
            Assert.AreEqual(identResult.Id, "SIS_WZPEW6AVX2YAJ5V6R5GQGJHXXMHBA7");
            Assert.IsTrue(identResult.Demo.Value);

        }

        [TestMethod, TestCategory("Model")]
        public void Test_Service_Identcontracts_1()
        {
            var json = File.ReadAllText("Data\\Model\\Services.Identcontracts.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<IdentContract>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var contract = data.List.First();
            Assert.AreEqual(contract.Id, "SIC_WNVNAA62B2Y7GEJVR5GQGW8EAN62A6");
            Assert.IsFalse(contract.Demo.Value);

        }

    }
}