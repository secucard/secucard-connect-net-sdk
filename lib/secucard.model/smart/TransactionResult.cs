namespace Secucard.Model.Smart
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.Payment;
    using Secucard.Model.Transport;

    [DataContract]
    public class TransactionResult : Status
    {

        [DataMember(Name = "transaction")]
        public Transaction Transaction;

        [DataMember(Name = "payment_method")]
        public string PaymentMethod;

        [DataMember(Name = "receipt")]
        public List<ReceiptLine> ReceiptLines;


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
