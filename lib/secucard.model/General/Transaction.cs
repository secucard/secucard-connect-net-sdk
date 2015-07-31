namespace Secucard.Model.General
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.Loyalty;

    [DataContract]
    public class Transaction : SecuObject
    {

        public override string ServiceResourceName
        {
            get { return "general.transactions"; }
        }       
        
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "details")]
        public Sale Details { get; set; }

        public DateTime? LastChange { get; set; }
        //public static final String TYPE_SALE = "sale";
        //public static final String TYPE_CHARGE = "charge";

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "last_change")]
        public string FormattedLastChange
        {
            get { return LastChange.ToDateTimeZone(); }
            set { LastChange = value.ToDateTime(); }
        }

    }
}