namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupaySofortService : PaymentService<SecupaySofort>
    {
        public static readonly ServiceMetaData<SecupaySofort> MetaData = new ServiceMetaData<SecupaySofort>("payment", "secupaysofort");

        protected override ServiceMetaData<SecupaySofort> GetMetaData()
        {
            return MetaData;
        }
    }
}
