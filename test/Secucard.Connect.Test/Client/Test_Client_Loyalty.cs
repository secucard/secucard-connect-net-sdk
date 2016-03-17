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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_Loyalty : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_LoyaltyCustomers_1()
        {
            StartupClientUser();

            var customerService = Client.Loyalty.CustomerLoyalty;
            var list = customerService.GetList(null);
        }
    }
}