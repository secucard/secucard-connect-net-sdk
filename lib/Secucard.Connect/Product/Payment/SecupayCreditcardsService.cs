namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayCreditcardsService : ProductService<SecupayCreditcard>
    {
        public static readonly ServiceMetaData<SecupayCreditcard> MetaData = new ServiceMetaData<SecupayCreditcard>("payment", "secupaycreditcards");

        protected override ServiceMetaData<SecupayCreditcard> GetMetaData()
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