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

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "name")]
        public string Name;

        [DataMember(Name = "length")]
        public int? Length;

        [DataMember(Name = "prefix")]
        public string Prefix;

        [DataMember(Name = "value")]
        public string Value;

        [DataMember(Name = "customer")]
        public Customer Customer;

        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard;

        [DataMember(Name = "valid")]
        public bool? Valid;

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
