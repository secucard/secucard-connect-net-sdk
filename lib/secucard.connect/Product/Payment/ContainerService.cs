namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Model.Payment;

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
