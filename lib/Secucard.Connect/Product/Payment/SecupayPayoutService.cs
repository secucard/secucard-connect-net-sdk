namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayPayoutService : PaymentService<SecupayPayout>
    {
        public static readonly ServiceMetaData<SecupayPayout> MetaData = new ServiceMetaData<SecupayPayout>("payment", "secupaypayout");

        protected override ServiceMetaData<SecupayPayout> GetMetaData()
        {
            return MetaData;
        }
    }
}
