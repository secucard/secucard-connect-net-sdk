namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Model.Document;

    public class DocumentService : ProductService<Document>
    {
        protected override ServiceMetaData<Document> CreateMetaData()
        {
            return new ServiceMetaData<Document>("document", "uploads");
        }
    }
}
