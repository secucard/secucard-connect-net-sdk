namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class CustomerService : ProductService<Customer>
    {
        protected override ServiceMetaData<Customer> CreateMetaData()
        {
            return new ServiceMetaData<Customer>("payment", "customers");
        }
        //public ObjectList<Customer> GetCustomers(QueryParams queryParams)
        //{
        //    return GetList<Customer>(queryParams);
        //}
        
        //public Customer CreateCustomer(Customer customer)
        //{
        //    return Create(customer);
        //}

        //public Customer UpdateCustomer(Customer customer)
        //{
        //    return Update(customer);
        //}

        //public void DeleteCustomer(string id)
        //{
        //    Delete<Customer>(id);
        //}


    }
}
