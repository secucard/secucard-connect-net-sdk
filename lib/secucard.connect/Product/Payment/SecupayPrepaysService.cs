namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayPrepaysService : ProductService<SecupayDebit>
    {
        protected override ServiceMetaData<SecupayDebit> CreateMetaData()
        {
            return new ServiceMetaData<SecupayDebit>("payment", "prepays");
        }
    }
}
