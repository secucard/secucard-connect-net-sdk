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
            StartupClientUser();

            var documentService = Client.GetService<UploadsService>();

            var upload = new Upload
            {
                Content = "base64encodeddata"
            };

            var id = documentService.Upload(upload);
            Assert.IsNotNull(id);
        }
    }
}