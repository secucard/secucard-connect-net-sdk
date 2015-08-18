namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayPrepaysService : ProductService<SecupayPrepay>
    {
        public static readonly ServiceMetaData<SecupayPrepay> META_DATA = new ServiceMetaData<SecupayPrepay>("payment", "secupayprepays");

        protected override ServiceMetaData<SecupayPrepay> GetMetaData()
        {
            return META_DATA;
        }
    }
}