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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_Stomp_1 : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_GeneralSkeletonStomp_1_GET()
        {
            StartupClientDevice();

            var service = Client.GetService<SkeletonsServiceStomp>();

            var queryParams = new QueryParams
            {
                Count = 2,
                ScrollExpire = "5m",
                SortOrder = new Dictionary<string, string>() {{"a", QueryParams.SortAsc}},
                Fields = new List<string> {"id", "a", "b"}
            };

            //TEST Heartbeat
            Thread.Sleep(2000);

            var list = service.GetList(queryParams);
            Assert.IsTrue(list.Count > 0);

            var skeleton = service.Get(list.List.First().Id);
            // ERROR: Assert.AreEqual(skeleton.A, "abc1");
        }
    }
}