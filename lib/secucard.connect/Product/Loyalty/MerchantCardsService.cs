namespace Secucard.Connect.Product.Loyalty
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class MerchantCardsService : ProductService<MerchantCard> {

        protected override ServiceMetaData<MerchantCard> CreateMetaData()
        {
            return new ServiceMetaData<MerchantCard>("loyalty", "merchantcards");
        }


    }
}
