namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ShippingInformation
    {
        [DataMember(Name = "carrier")]
        public string Carrier { get; set; }

        [DataMember(Name = "tracking_id")]
        public string TrackingId { get; set; }

        [DataMember(Name = "invoice_number")]
        public string InvoiceNumber { get; set; }

        public override string ToString()
        {
            return "ShippingInformation{" +
                   "carrier='" + this.Carrier + '\'' +
                   ", tracking_id='" + this.TrackingId + '\'' +
                   ", invoice_number='" + this.InvoiceNumber + '\'' +
                   '}';
        }
    }
}