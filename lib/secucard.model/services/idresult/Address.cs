namespace Secucard.Model.Services.Idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Address
    {
        [DataMember(Name = "postal_code")]
        public ValueClass postalCode { get; set; }

        [DataMember(Name = "country")]
        public ValueClass country { get; set; }

        [DataMember(Name = "city")]
        public ValueClass city { get; set; }

        [DataMember(Name = "street")]
        public ValueClass street { get; set; }

        [DataMember(Name = "street_number")]
        public ValueClass streetNumber { get; set; }

        public override string ToString()
        {
            return "Address{" +
                   "zipcode=" + postalCode +
                   ", country=" + country +
                   ", city=" + city +
                   ", street=" + street +
                   ", streetNumber=" + streetNumber +
                   '}';
        }
    }
}