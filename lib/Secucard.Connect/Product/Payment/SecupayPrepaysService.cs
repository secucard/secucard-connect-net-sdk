namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayPrepaysService : PaymentService<SecupayPrepay>
    {
        public static readonly ServiceMetaData<SecupayPrepay> MetaData = new ServiceMetaData<SecupayPrepay>("payment", "secupayprepays");

        protected override ServiceMetaData<SecupayPrepay> GetMetaData()
        {
            return MetaData;
        }
    }
}
