namespace Secucard.Connect.Test.Client
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Document;
    using Secucard.Connect.Product.Document.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_DocumentService : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_DocumentService_1()
        {
            var client = SecucardConnect.Create(ClientConfigurationUser);
            client.AuthEvent += ClientOnAuthEvent;
            client.Connect();

            var documentService = client.GetService<DocumentService>();

            var document = new Document
            {
                Content = "base64encodeddata"
            };

            var docPost = documentService.Create(document);
            Assert.IsNotNull(docPost);

            var docGet = documentService.Get(docPost.Id);
            Assert.IsNotNull(docGet);
        }
    }
}