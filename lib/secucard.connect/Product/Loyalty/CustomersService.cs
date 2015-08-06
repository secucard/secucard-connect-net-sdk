namespace Secucard.Connect.Product.Loyalty
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class CustomersService : ProductService<Customer>
    {

        protected override ServiceMetaData<Customer> CreateMetaData()
        {
            return new ServiceMetaData<Customer>("loyalty", "customers");
        }


    }
}
