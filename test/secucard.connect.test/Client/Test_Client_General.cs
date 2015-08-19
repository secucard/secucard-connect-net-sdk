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

namespace Secucard.Connect.Test.Client
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.General;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_General : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralStores()
        {
            StartupClientDevice();
            var store = Client.General.Stores.Get("STO_3SGKT879YSRC27NEJSG6BJ85P4CKP8");
            Assert.IsNotNull(store);

            var bytes = store.Logo.GetContents();
            Assert.IsNotNull(bytes);
            Assert.AreEqual(bytes.Length, 11247);
        }


        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralAccountDevices()
        {
            StartupClientDevice();
            var list = Client.General.Accountdevices.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralAccounts()
        {
            StartupClientDevice();
            var list = Client.General.Accounts.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralTransactions()
        {
            StartupClientDevice();
            var list = Client.General.GeneralTransactions.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralMerchants()
        {
            StartupClientDevice();
            var list = Client.General.Merchants.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralNews()
        {
            StartupClientDevice();
            var list = Client.General.News.GetList(null);
            Assert.IsNotNull(list);
        }

        [TestMethod, TestCategory("Client")]
        public void Test_Client_PublicMerchants()
        {
            StartupClientDevice();
            var list = Client.General.Publicmerchants.GetList(null);
            Assert.IsNotNull(list);
        }


        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeleton_1()
        {
            StartupClientDevice();

            var skeletonService = Client.GetService<SkeletonsService>();

            var skeletons = skeletonService.GetList(null);

            skeletonService.CreateEvent();

            Thread.Sleep(4000);
        }
    }
}