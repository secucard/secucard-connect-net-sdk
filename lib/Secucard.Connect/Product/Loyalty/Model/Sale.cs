namespace Secucard.Connect.Product.Loyalty.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Sale : SecuObject
    {
        public DateTime? Created;
        public DateTime? LastChange;

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "balance_amount")]
        public int BalanceAmount { get; set; }

        [DataMember(Name = "balance_points")]
        public int BalancePoints { get; set; }

        [DataMember(Name = "currency")]
        public string Currency;

        [DataMember(Name = "bonus")]
        public List<Bonus> Bonus { get; set; }

        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [DataMember(Name = "cardgroup")]
        public CardGroup Cardgroup { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "description_raw")]
        public string DescriptionRaw { get; set; }

        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "store")]
        public Store Store { get; set; }

        [DataMember(Name = "last_change")]
        public string FormattedLastChange
        {
            get { return LastChange.ToDateTimeZone(); }
            set { LastChange = value.ToDateTime(); }
        }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
    }
}