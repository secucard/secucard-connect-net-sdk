namespace Secucard.Connect.Product.Loyalty
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class MerchantCardsService : ProductService<MerchantCard>
    {
        public static readonly ServiceMetaData<MerchantCard> MetaData = new ServiceMetaData<MerchantCard>("loyalty",
            "merchantcards");

        protected override ServiceMetaData<MerchantCard> GetMetaData()
        {
            return MetaData;
        }
    }
}