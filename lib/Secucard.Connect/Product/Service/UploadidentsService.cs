namespace Secucard.Connect.Product.Service
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Service.Model.services;

    /// <summary>
    /// Implements the services/uploadidents operations.
    /// </summary>
    public class UploadidentsService : ProductService<Uploadident>
    {
        public static readonly ServiceMetaData<Uploadident> MetaData = new ServiceMetaData<Uploadident>("payment",
            "uploadidents");

        protected override ServiceMetaData<Uploadident> GetMetaData()
        {
            return MetaData;
        }
    }
}