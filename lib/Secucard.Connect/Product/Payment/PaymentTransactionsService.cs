namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class PaymentTransactionsService : ProductService<PaymentTransaction>
    {
        public static readonly ServiceMetaData<PaymentTransaction> MetaData = new ServiceMetaData<PaymentTransaction>("payment",
            "transactions");

        protected override ServiceMetaData<PaymentTransaction> GetMetaData()
        {
            return MetaData;
        }
    }
}