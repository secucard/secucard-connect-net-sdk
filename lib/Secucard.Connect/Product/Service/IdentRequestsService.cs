namespace Secucard.Connect.Product.Service
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Service.Model.services;

    /// <summary>
    /// Implements the services/identrequest operations.
    /// </summary>
    public class IdentRequestsService : ProductService<IdentRequest>
    {
        public static readonly ServiceMetaData<IdentRequest> MetaData = new ServiceMetaData<IdentRequest>("payment",
            "identrequests");

        protected override ServiceMetaData<IdentRequest> GetMetaData()
        {
            return MetaData;
        }
    }
}