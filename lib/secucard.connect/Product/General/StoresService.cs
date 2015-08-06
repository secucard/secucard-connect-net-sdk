namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class StoresService : ProductService<Store>
    {
        protected override ServiceMetaData<Store> CreateMetaData()
        {
            return new ServiceMetaData<Store>("general", "store");
        }
    }
}