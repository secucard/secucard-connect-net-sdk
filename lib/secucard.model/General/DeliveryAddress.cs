namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class DeliveryAddress : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "general.deliveryaddresses"; }
        }

        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "zipcode")]
        public string ZipCode { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }
    }
}