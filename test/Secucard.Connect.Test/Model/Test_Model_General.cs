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
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_General : Test_Base
    {
        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_0_doc()
        {
            var json = File.ReadAllText("Data\\Model\\General.Accounts.0.doc.json");
            var data_accounts = JsonSerializer.DeserializeJson<ObjectList<Account>>(json);
            Assert.AreEqual(data_accounts.List.Count, 1);
            Assert.AreEqual(data_accounts.List.First().Username, "j.doe");
            Assert.AreEqual(data_accounts.List.First().Assignment.First().IsOwner, true);
            Assert.AreEqual(data_accounts.List.First().Assignment.First().Type, "assignment");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Accounts.1.json");
            var account = JsonSerializer.DeserializeJson<Account>(json);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_2()
        {
            var json = File.ReadAllText("Data\\Model\\General.Accounts.2.json");
            var data_accounts = JsonSerializer.DeserializeJson<ObjectList<Account>>(json);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accounts_3()
        {
            var json = File.ReadAllText("Data\\Model\\General.Accounts.3.json");
            var data_accounts = JsonSerializer.DeserializeJson<ObjectList<Account>>(json);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Contact_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Contacts.1.json");
            var contact = JsonSerializer.DeserializeJson<Contact>(json);
            Assert.AreEqual(contact.Forename, "Dummy");
            Assert.AreEqual(contact.Surname, "Merchant1");
            Assert.AreEqual(contact.Name, "Dummy Merchant1");
            Assert.AreEqual(contact.Salutation, "Herr");
            Assert.AreEqual(contact.Id, "CNT_2V8EC9RTT2Y772BFB5GQGNH6NM8UA6");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Accountdevices_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Accountdevices.1.json");
            var devices = JsonSerializer.DeserializeJson<ObjectList<AccountDevice>>(json);
            Assert.IsTrue(devices.Count > 1);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_App_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Apps.1.json");
            var apps = JsonSerializer.DeserializeJson<ObjectList<App>>(json);
            Assert.IsTrue(apps.Count > 1);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Deliveryaddress_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Deliveryaddresses.1.json");
            var apps = JsonSerializer.DeserializeJson<ObjectList<DeliveryAddress>>(json);
            Assert.IsTrue(apps.Count > 1);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Device_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Devices.2.json");
            var devices = JsonSerializer.DeserializeJson<ObjectList<Device>>(json);
            Assert.AreEqual(devices.Count, 0);
            Assert.IsNull(devices.List);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_News_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.News.1.json");
            var news = JsonSerializer.DeserializeJson<ObjectList<News>>(json);
            Assert.IsTrue(news.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Publicmerchante_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Publicmerchant.1.json");
            var merchants = JsonSerializer.DeserializeJson<ObjectList<PublicMerchant>>(json);
            Assert.IsTrue(merchants.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Stores_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Stores.1.json");
            var stores = JsonSerializer.DeserializeJson<ObjectList<Store>>(json);
            Assert.IsTrue(stores.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Transactions_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Transactions.1.json");
            var stores = JsonSerializer.DeserializeJson<ObjectList<Transaction>>(json);
            Assert.IsTrue(stores.Count > 0);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_General_Skeleton_1()
        {
            var json = File.ReadAllText("Data\\Model\\General.Skeleton.1.json");
            var skeletons = JsonSerializer.DeserializeJson<ObjectList<Skeleton>>(json);
            Assert.IsTrue(skeletons.Count > 0);
        }
    }
}