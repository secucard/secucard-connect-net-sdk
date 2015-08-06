namespace Secucard.Connect.Product.Document
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Document.Model;

    public class DocumentService : ProductService<Document>
    {
        protected override ServiceMetaData<Document> CreateMetaData()
        {
            return new ServiceMetaData<Document>("document", "uploads");
        }
    }
}
