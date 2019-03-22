namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class PaymentTransactionDetails
    {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "cleared")]
        public string Cleared { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "status_text")]
        public string StatusText { get; set; }

        [DataMember(Name = "status_simple")]
        public string StatusSimple { get; set; }

        [DataMember(Name = "status_text_simple")]
        public string StatusTextSimple { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "description_raw")]
        public string DescriptionRaw { get; set; }

        public override string ToString()
        {
            return "PaymentTransactionDetails{" +
                   "Amount='" + this.Amount + "', " +
                   "Cleared='" + this.Cleared + "', " +
                   "Status='" + this.Status + "', " +
                   "StatusText='" + this.StatusText + "', " +
                   "StatusSimple='" + this.StatusSimple + "', " +
                   "StatusTextSimple='" + this.StatusTextSimple + "', " +
                   "Description='" + this.Description + "', " +
                   "DescriptionRaw='" + this.DescriptionRaw + "', " +
                   "} ";
        }
    }
}