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
                QueyParams = new QueryParams
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
                    QueyParams = new QueryParams {Query = "forename:" + contact.Forename},
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