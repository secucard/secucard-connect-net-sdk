namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class CustomerPaymentService : ProductService<Customer>
    {
        public static readonly ServiceMetaData<Customer> MetaData = new ServiceMetaData<Customer>("payment", "customers");

        protected override ServiceMetaData<Customer> GetMetaData()
        {
            return MetaData;
        }
    }
}