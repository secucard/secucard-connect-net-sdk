namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;
    using Secucard.Model.Loyalty;

    [DataContract]
    public class Ident : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "smart.idents"; }
        }

        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "length")]
        public int? Length { get; set; }

        [DataMember(Name = "prefix")]
        public string Prefix { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "valid")]
        public bool? Valid { get; set; }

        public override string ToString()
        {
            return "Ident {" +
                   "type='" + Type + '\'' +
                   ", name='" + Name + '\'' +
                   ", length=" + Length +
                   ", prefix='" + Prefix + '\'' +
                   ", value='" + Value + '\'' +
                   ", customer=" + Customer +
                   ", merchantCard=" + MerchantCard +
                   ", valid=" + Valid +
                   "} " + base.ToString();
        }
    }
}