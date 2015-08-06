namespace secucard.connect.test.Rest
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Document.Model;
    using Secucard.Connect.rest;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Document_Uploads : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Document_Uploads_1_POST_GET()
        {
            var document = new Document
            {
                Content = "base64encodeddata"
            };

            // POST 
            var request = new RestRequest
            {
                Token = AccessToken,
                Object = document,
                PageUrl = "Document/Uploads",
                Host = "core-dev10.secupay-ag.de"
            };

            var docPost = RestService.PostObject<Document>(request);


            Thread.Sleep(1000);

            // GET by id
            var requestGet = new RestRequest
            {
                Token = AccessToken,
                Id = docPost.Id,
                PageUrl = "Document/Uploads",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetObject<Document>(requestGet);
        }
    }
}