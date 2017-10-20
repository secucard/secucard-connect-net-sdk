namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class SecupayInvoicesService : PaymentService<SecupayInvoice>
    {
        public static readonly ServiceMetaData<SecupayInvoice> MetaData = new ServiceMetaData<SecupayInvoice>("payment", "secupayinvoices");

        protected override ServiceMetaData<SecupayInvoice> GetMetaData()
        {
            return MetaData;
        }
    }
}