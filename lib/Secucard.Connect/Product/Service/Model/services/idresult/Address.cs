namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Address
    {
        [DataMember(Name = "postal_code")]
        public ValueClass PostalCode { get; set; }

        [DataMember(Name = "country")]
        public ValueClass Country { get; set; }

        [DataMember(Name = "city")]
        public ValueClass City { get; set; }

        [DataMember(Name = "street")]
        public ValueClass Street { get; set; }

        [DataMember(Name = "street_number")]
        public ValueClass StreetNumber { get; set; }

        public override string ToString()
        {
            return "Address{" +
                   "zipcode=" + PostalCode +
                   ", country=" + Country +
                   ", city=" + City +
                   ", street=" + Street +
                   ", streetNumber=" + StreetNumber +
                   '}';
        }
    }
}