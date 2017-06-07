namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class ContainersService : ProductService<Container>
    {
        public static readonly ServiceMetaData<Container> MetaData = new ServiceMetaData<Container>("payment", "containers");

        protected override ServiceMetaData<Container> GetMetaData()
        {
            return MetaData;
        }
    }
}