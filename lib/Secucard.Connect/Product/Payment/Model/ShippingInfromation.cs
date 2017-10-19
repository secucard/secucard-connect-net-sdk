namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ShippingInformation
    {
        [DataMember(Name = "provider")]
        public string Provider { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "invoice_number")]
        public string InvoiceNumber { get; set; }

        public override string ToString()
        {
            return "ShippingInformation{" +
                   "provider='" + this.Provider + '\'' +
                   ", number='" + this.Number + '\'' +
                   ", invoice_number='" + this.InvoiceNumber + '\'' +
                   '}';
        }
    }
}