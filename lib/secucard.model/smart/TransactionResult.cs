namespace Secucard.Model.Smart
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.Transport;

    [DataContract]
    public class TransactionResult : Status
    {
        [DataMember(Name = "payment_method")]
        public string PaymentMethod { get; set; }

        [DataMember(Name = "receipt")]
        public List<ReceiptLine> ReceiptLines { get; set; }

        [DataMember(Name = "transaction")]
        public Transaction Transaction { get; set; }

        public override string ToString()
        {
            return "Result{" +
                   "transaction=" + Transaction +
                   ", status='" + StatusProp + '\'' +
                   ", error='" + Error + '\'' +
                   ", paymentMethod='" + PaymentMethod + '\'' +
                   ", receiptLines=" + ReceiptLines +
                   '}';
        }
    }
}