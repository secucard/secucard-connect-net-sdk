namespace Secucard.Connect.Product.Document
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Document.Model;

    public class UploadsService : ProductService<Upload>
    {
        public static readonly ServiceMetaData<Upload> MetaData = new ServiceMetaData<Upload>("document", "uploads");

        protected override ServiceMetaData<Upload> GetMetaData()
        {
            return MetaData;
        }

        /// <summary>
        ///  Upload the given document and returns the new id for the upload.
        ///  Note: the uploaded content should be base64 encoded.
        /// </summary>
        public string Upload(Upload content)
        {
            Upload result = Execute<Upload>(null, null, null, content, null);
            return result.Id;
        }
    }
}