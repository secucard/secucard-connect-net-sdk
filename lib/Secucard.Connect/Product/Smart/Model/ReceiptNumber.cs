namespace Secucard.Connect.Product.Smart.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ReceiptNumber
    {
        [DataMember(Name = "receipt_number")]
        public string receiptNumber { get; set; }

        public override string ToString()
        {
            return "ReceiptNumber{" +
                   "receipt_number='" + receiptNumber +
                   '}';
        }
    }
}