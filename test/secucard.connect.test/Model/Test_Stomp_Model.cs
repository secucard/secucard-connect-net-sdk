namespace Secucard.Connect.Test.Model
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Stomp_Model : Test_Base
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Stomp_Response_1()
        {
            var json = File.ReadAllText("Data\\Model\\Stomp.Response.1.json");

            var response = new Response(json);
            Assert.IsTrue(response.Data.Contains("result"));
            var result = JsonSerializer.DeserializeJson<StompResult>(response.Data);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Stomp_Response_2()
        {
            var json = File.ReadAllText("Data\\Model\\Stomp.Response.2.json");

            var response = new Response(json);
            Assert.IsTrue(response.Data.Contains("SKL_DX98ZE00KZT8TJQZXGHWGY8ZNHXTTK"));
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Stomp_Event_1()
        {
            var json = File.ReadAllText("Data\\Model\\Event.Pushs.1.json");

            var response = JsonSerializer.DeserializeToDictionary(json);
            Assert.IsTrue(response.ContainsKey("object"));
            Assert.AreEqual(response["object"], "event.pushs");
            Assert.AreEqual(response["type"], "display");

            var e = JsonSerializer.DeserializeJson<Event<Notification>>(json);
            Assert.AreEqual(e.Data.Text, "DEMO Aktivität mit mehr Text");

        }
    }
}