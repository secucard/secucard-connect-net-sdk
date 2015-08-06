namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class MerchantsService : ProductService<Merchant>
    {

        protected override ServiceMetaData<Merchant> CreateMetaData()
        {
                return new ServiceMetaData<Merchant>("general", "merchants");
        }
    }
}
