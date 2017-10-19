namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Data
    {
        [DataMember(Name = "bankname")]
        public string Bankname { get; set; }

        [DataMember(Name = "bic")]
        public string Bic { get; set; }

        [DataMember(Name = "iban")]
        public string Iban { get; set; }

        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        public override string ToString()
        {
            return "Data{" +
                   "owner='" + this.Owner + '\'' +
                   ", iban='" + this.Iban + '\'' +
                   ", bic='" + this.Bic + '\'' +
                   ", bankname='" + this.Bankname + '\'' +
                   '}';
        }
    }
}