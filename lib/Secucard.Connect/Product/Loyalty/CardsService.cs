namespace Secucard.Connect.Product.Loyalty
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class CardsService : ProductService<Card>
    {
        public static readonly ServiceMetaData<Card> MetaData = new ServiceMetaData<Card>("general", "cards");

        protected override ServiceMetaData<Card> GetMetaData()
        {
            return MetaData;
        }
    }
}