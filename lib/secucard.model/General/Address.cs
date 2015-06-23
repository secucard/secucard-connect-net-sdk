namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Address
    {

        [DataMember(Name = "street")]
        public string street { get; set; }

        [DataMember(Name = "street_number")]
        public string streetNumber { get; set; }

        [DataMember(Name = "postal_code")]
        public string postalCode { get; set; }

        [DataMember(Name = "city")]
        public string city { get; set; }

        [DataMember(Name = "country")]
        public string country { get; set; } // ISO 3166 country code like DE

        public override string ToString()
        {
            return "Address{" +
                   "street='" + street + '\'' +
                   ", streetNumber='" + streetNumber + '\'' +
                   ", postalCode='" + postalCode + '\'' +
                   ", city='" + city + '\'' +
                   ", country='" + country + '\'' +
                   '}';
        }
    }
}
