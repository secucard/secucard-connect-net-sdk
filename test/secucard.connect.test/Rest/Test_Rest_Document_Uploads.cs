namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Rest;
    using Secucard.Model.Document;
    using Secucard.Model.General;

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
                Token = Token.AccessToken,
                Object = document,
                PageUrl = "Document/Uploads",
                Host = "core-dev10.secupay-ag.de"
            };

            var docPost = RestService.PostObject<Document>(request);


            // GET by id
            var requestGet = new RestRequest
            {
                Token = Token.AccessToken,
                Id = docPost.Id,
                PageUrl = "Document/Uploads",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetObject<Document>(requestGet);
        }
    }
}