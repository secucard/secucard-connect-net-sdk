namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Mandate
    {
        public const int MandateStatusUnkown = 0;
        public const int MandateStatusRequest = 1;
        public const int MandateStatusCancelled = 2;
        public const int MandateStatusPreliminary = 3;
        public const int MandateStatusOk = 10;

        [DataMember(Name = "iban")]
        public string Iban { get; set; }

        [DataMember(Name = "bic")]
        public string Bic { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "identification")]
        public string Identification { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "sepa_mandate_id")]
        public string SepaMandateId { get; set; }

        public override string ToString()
        {
            return "Mandate{" +
                   ", iban=" + this.Iban +
                   ", bic=" + this.Bic +
                   ", type=" + this.Type +
                   ", identification=" + this.Identification +
                   ", status=" + this.Status +
                   ", sepa_mandate_id=" + this.SepaMandateId +
                   '}';
        }
    }
}