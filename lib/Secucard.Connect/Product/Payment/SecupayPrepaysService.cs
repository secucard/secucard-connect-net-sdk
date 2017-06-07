namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayPrepaysService : ProductService<SecupayPrepay>
    {
        public static readonly ServiceMetaData<SecupayPrepay> MetaData = new ServiceMetaData<SecupayPrepay>("payment", "secupayprepays");

        protected override ServiceMetaData<SecupayPrepay> GetMetaData()
        {
            return MetaData;
        }
        
        public bool Cancel(string prepayId)
        {
            if (Execute<Transaction>(prepayId, "cancel", null, null, null) == null)
                return false;
            else
                return true;
        }
    }
}
