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
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Contacts : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Contacts_1_GET()
        {
            var request = new RestRequest
            {
                Token = AccessToken,
                QueryParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0
                },
                PageUrl = "General/Contacts",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetList<Contact>(request);

            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Contacts_2_POST()
        {
            var contact = new Contact
            {
                Id = "CNT_" + Guid.NewGuid(),
                Forename = "TestForename" + DateTime.Now.Ticks,
                Surname = "TestSurename" + DateTime.Now.Ticks,
                Gender = Contact.GENDER_MALE
            };

            // POST 
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    Object = contact,
                    PageUrl = "General/Contacts",
                    Host = "core-dev10.secupay-ag.de"
                };

                var data = RestService.PostObject<Contact>(request);
            }


            // GET with query
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    QueryParams = new QueryParams {Query = "forename:" + contact.Forename},
                    PageUrl = "General/Contacts",
                    Host = "core-dev10.secupay-ag.de"
                };

                var data = RestService.GetList<Contact>(request);

                Assert.AreEqual(data.List.First().Forename, contact.Forename, "object not in storage arrived.");
            }


            return;
            // PUT does not work currently.


            // Change some data
            contact.Forename = "Changed" + DateTime.Now.Ticks;

            // PUT
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    Object = contact,
                    PageUrl = "General/Contacts",
                    Host = "core-dev10.secupay-ag.de"
                };

                var data = RestService.PutObject<Contact>(request);

                Assert.AreEqual(data.Forename, contact.Forename);
            }


            //// GET with query
            //{
            //    var request = new RestRequest
            //    {
            //        Token = Token,
            //        QueyParams = new QueryParams { Query = "forename:" + contact.Forename },
            //        PageUrl = "General/Contacts",
            //        Host = "core-dev10.secupay-ag.de"
            //    };

            //    var data = RestService.GetList<Contact>(request);

            //    Assert.AreEqual(data.List.First().Forename, contact.Forename, "changes not in storage arrived.");
            //}
        }
    }
}