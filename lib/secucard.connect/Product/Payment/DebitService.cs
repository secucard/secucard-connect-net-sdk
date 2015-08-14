namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayDebitsService : ProductService<SecupayDebit>
    {
        protected override ServiceMetaData<SecupayDebit> CreateMetaData()
        {
            return new ServiceMetaData<SecupayDebit>("payment", "debits");
        }
    }
}
