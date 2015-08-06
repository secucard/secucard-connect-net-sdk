namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class ContainerService : ProductService<Container>
    {
        protected override ServiceMetaData<Container> CreateMetaData()
        {
            return new ServiceMetaData<Container>("payment", "containers");
        }

        //public Container CreateContainer(Container container)
        //{
        //    return Create(container);
        //}

        
    }
}
