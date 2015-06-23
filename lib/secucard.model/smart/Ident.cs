namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;
    using Secucard.Model.Loyalty;

    [DataContract]
    public class Ident : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "smart.idents"; }
        }

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "name")]
        public string Name;

        [DataMember(Name = "length")]
        public int Length;

        [DataMember(Name = "prefix")]
        public string Prefix;

        [DataMember(Name = "value")]
        public string Value;

        [DataMember(Name = "customer")]
        public Customer Customer;

        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard;

        [DataMember(Name = "valid")]
        public bool Valid;

        //public Ident() {
        //}

        //public Ident(Ident ident) {
        //  this.type = ident.getType();
        //  this.name = ident.getName();
        //  this.length = ident.getLength();
        //  this.prefix = ident.getPrefix();
        //  this.value = ident.getValue();
        //  this.merchantCard = ident.getMerchantCard();
        //  this.valid = ident.isValid();
        //}

        //public Ident(string type, string value) {
        //  this.type = type;
        //  this.value = value;
        //}


        //     /**
        //* Selects a indent of a given id from a list of idents.
        //*
        //* @param id     The ident id.
        //* @param idents The idents to query.
        //* @return The found ident or null.
        //*/
        //     public static Ident Find(string id, List<Ident> idents)
        //     {
        //         return idents == null ? null : idents.FirstOrDefault(ident => ident.Id == id);
        //     }


        public override string ToString()
        {
            return "Ident{" +
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
