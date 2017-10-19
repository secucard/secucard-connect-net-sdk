namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayCreditcardsService : PaymentService<SecupayCreditcard>
    {
        public static readonly ServiceMetaData<SecupayCreditcard> MetaData = new ServiceMetaData<SecupayCreditcard>("payment", "secupaycreditcards");

        protected override ServiceMetaData<SecupayCreditcard> GetMetaData()
        {
            return MetaData;
        }
    }
}