namespace Secucard.Connect.Product.General
{
    using Secucard.Model;
    using Secucard.Model.Payment;

    public class CustomerService : AbstractService
    {
        public ObjectList<Customer> GetCustomers(QueryParams queryParams)
        {
            return GetList<Customer>(queryParams);
        }
        
        public Customer CreateCustomer(Customer customer)
        {
            return Create(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return Update(customer);
        }

        public void DeleteCustomer(string id)
        {
            Delete<Customer>(id);
        }


    }
}
