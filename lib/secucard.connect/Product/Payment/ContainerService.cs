namespace Secucard.Connect.Product.General
{
    using Secucard.Model.Payment;

    public class ContainerService : AbstractService
    {
        public Container CreateContainer(Container container)
        {
            return Create(container);
        }

        
    }
}
