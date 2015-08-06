namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Container : SecuObject
    {
        public const string TYPE_BANK_ACCOUNT = "bank_account";

        public override string ServiceResourceName
        {
            get { return "payment.containers"; }
        }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

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

        public DateTime? Created { get; set; }

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return Updated.ToDateTimeZone(); }
            set { Updated = value.ToDateTime(); }
        }

        public DateTime? Updated { get; set; }

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