namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayDebitsService : PaymentService<SecupayDebit>
    {
        public static readonly ServiceMetaData<SecupayDebit> MetaData = new ServiceMetaData<SecupayDebit>("payment", "secupaydebits");

        protected override ServiceMetaData<SecupayDebit> GetMetaData()
        {
            return MetaData;
        }
    }
}