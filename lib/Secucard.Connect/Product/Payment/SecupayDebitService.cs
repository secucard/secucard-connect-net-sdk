namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayDebitsService : ProductService<SecupayDebit>
    {
        public static readonly ServiceMetaData<SecupayDebit> MetaData = new ServiceMetaData<SecupayDebit>("payment", "secupaydebits");

        protected override ServiceMetaData<SecupayDebit> GetMetaData()
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