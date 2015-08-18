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

namespace Secucard.Connect.Test.Rest
{
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Stores : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_1_GET()
        {
            var request = new RestRequest
            {
                Method = WebRequestMethods.Http.Get,
                Token = AccessToken,
                QueryParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0,
                },
                PageUrl = "General/Stores",
                Host = "core-dev10.secupay-ag.de"
            };

            var list = RestService.GetList<Store>(request);

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_2_GET()
        {
            {
                var request = new RestRequest
                {
                    Method = WebRequestMethods.Http.Get,
                    Token = AccessToken,
                    Url =
                        "https://core-dev10.secupay-ag.de/app.core.connector/ds_g/1822a1ccd76984b4a7e9536884b75fedf9e06a5a"
                };

                var stream = RestService.GetStream(request);

                Assert.IsNotNull(stream);
            }
        }
    }
}