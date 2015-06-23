
namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Data
    {
        [DataMember(Name = "owner")]
        public string Owner;

        [DataMember(Name = "iban")]
        public string Iban;

        [DataMember(Name = "bic")]
        public string Bic;

        [DataMember(Name = "bankname")]
        public string Bankname;

        //public Data() {
        //}

        //public Data(string iban) {
        //    this.iban = iban;
        //}




        public override string ToString()
        {
            return "Data{" +
                   "owner='" + Owner + '\'' +
                   ", iban='" + Iban + '\'' +
                   ", bic='" + Bic + '\'' +
                   ", bankname='" + Bankname + '\'' +
                   '}';
        }
    }
}
