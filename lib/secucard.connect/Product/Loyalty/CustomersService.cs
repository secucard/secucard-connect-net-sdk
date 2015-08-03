using Secucard.Connect.Client;
using Secucard.Model.Loyalty;

public class CustomersService : ProductService<Customer>
{

    protected override ServiceMetaData<Customer> CreateMetaData()
    {
        return new ServiceMetaData<Customer>("loyalty", "customers");
    }


}
