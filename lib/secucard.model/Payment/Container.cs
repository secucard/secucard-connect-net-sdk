namespace Secucard.Model.Payment
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class Container : SecuObject
    {

        public override string ServiceResourceName
        {
            get { return "payment.containers"; }
        }
        //@JsonIgnore
        //public static final String TYPE_BANK_ACCOUNT = "bank_account";

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "private")]
        public Data PrivateData { get; set; }

        [DataMember(Name = "public")]
        public Data PublicData { get; set; }

        [DataMember(Name = "assign")]
        public Customer Assigned { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created;

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return Updated.ToDateTimeZone(); }
            set { Updated = value.ToDateTime(); }
        }
        public DateTime? Updated;

        [DataMember(Name = "contract")]
        public Contract Contract;

        public override string ToString()
        {
            return "Container{" +
                   "merchant=" + Merchant +
                   ", privateData=" + PrivateData +
                   ", publicData=" + PublicData +
                   ", type='" + Type + '\'' +
                   ", created=" + Created +
                   ", updated=" + Updated +
                   '}';
        }
    }
}
