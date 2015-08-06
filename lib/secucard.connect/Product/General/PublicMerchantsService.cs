namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class PublicMerchantsService : ProductService<PublicMerchant>
    {
        protected override ServiceMetaData<PublicMerchant> CreateMetaData()
        {
            return new ServiceMetaData<PublicMerchant>("general", "publicmerchants");
        }
    }
}