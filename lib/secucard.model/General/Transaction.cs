using System;
using Secucard.Model.Loyalty;

namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Transaction : SecuObject
    {
        //public static final String TYPE_SALE = "sale";
        //public static final String TYPE_CHARGE = "charge";

        [DataMember(Name = "merchant")]
        public Merchant Merchant;

        [DataMember(Name = "amount")]
        public int Amount;

        [DataMember(Name = "last_change")]
        public string FormattedLastChange
        {
            get { return LastChange.ToDateTimeZone(); }
            set { LastChange = value.ToDateTime(); }
        }
        public DateTime? LastChange;

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "details")]
        public Sale Details;

        [DataMember(Name = "currency")]
        public string Currency;


        public override string SecuObjectName
        {
            get { return "general.transactions"; }
        }
    }
}
