using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Secucard.Model.General;

namespace Secucard.Model.Loyalty
{
    [DataContract]
    public class Sale : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "loyalty.sales"; }
        }

        [DataMember(Name = "amount")]
        public int Amount;

        [DataMember(Name = "last_change")]
        public string FormattedLastChange
        {
            get { return LastChange.ToDateTimeZone(); }
            set { LastChange = value.ToDateTime(); }
        }
        public DateTime? LastChange;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        } 
        public DateTime? Created;

        [DataMember(Name = "status")]
        public int Status;

        [DataMember(Name = "description")]
        public string Description;

        [DataMember(Name = "description_raw")]
        public string DescriptionRaw;

        [DataMember(Name = "store")]
        public Store Store;

        [DataMember(Name = "card")]
        public Card Card;

        [DataMember(Name = "cardgroup")]
        public CardGroup Cardgroup;

        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard;

        [DataMember(Name = "balance_amount")]
        public int BalanceAmount;

        [DataMember(Name = "balance_points")]
        public int BalancePoints;

        //TODO:
        //public Currency currency;

        [DataMember(Name = "created_for_merchant")]
        public List<Bonus> Bonus;


    }
}
