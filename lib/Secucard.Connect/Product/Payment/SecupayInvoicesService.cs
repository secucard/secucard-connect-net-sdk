namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayInvoicesService : ProductService<SecupayInvoice>
    {
        public static readonly ServiceMetaData<SecupayInvoice> MetaData = new ServiceMetaData<SecupayInvoice>("payment", "secupayinvoices");

        protected override ServiceMetaData<SecupayInvoice> GetMetaData()
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