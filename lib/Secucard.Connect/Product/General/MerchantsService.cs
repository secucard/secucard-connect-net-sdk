namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class MerchantsService : ProductService<Merchant>
    {
        public static readonly ServiceMetaData<Merchant> MetaData = new ServiceMetaData<Merchant>("general", "merchants");

        protected override ServiceMetaData<Merchant> GetMetaData()
        {
            return MetaData;
        }
    }
}