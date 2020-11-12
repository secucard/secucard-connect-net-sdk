namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayInvoice : Transaction
    {
        [DataMember(Name = "transfer_purpose")]
        public string TransferPurpose { get; set; }

        [DataMember(Name = "transfer_account")]
        public TransferAccount TransferAccount { get; set; }

        public override string ToString()
        {
            return "SecupayInvoice{" +
                   "transferPurpose='" + this.TransferPurpose + '\'' +
                   ", transactionStatus='" + this.TransactionStatus + '\'' +
                   ", transferAccount=" + TransferAccount +
                   "} " + base.ToString();
        }
    }
}