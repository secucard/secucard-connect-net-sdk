namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    [System.Obsolete("Not used any more", true)]
    public class PublicMerchantsService : ProductService<PublicMerchant>
    {
        public static readonly ServiceMetaData<PublicMerchant> MetaData = new ServiceMetaData<PublicMerchant>(
            "general", "publicmerchants");

        protected override ServiceMetaData<PublicMerchant> GetMetaData()
        {
            return MetaData;
        }
    }
}