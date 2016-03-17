/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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