namespace Secucard.Model.Loyalty
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class MerchantCard : SecuObject
    {
        public override string SecuObjectName { get { return "loyalty.merchantcards"; } }

        [DataMember(Name = "merchant")]
        public Merchant Merchant;

        [DataMember(Name = "created_for_merchant")]
        public Merchant CreatedForMerchant;

        [DataMember(Name = "card")]
        public Card Card;

        [DataMember(Name = "created_for_store")]
        public Store CreatedForStore;

        [DataMember(Name = "is_base_card")]
        public bool IsBaseCard;

        [DataMember(Name = "cardgroup")]
        public CardGroup Cardgroup;

        [DataMember(Name = "customer")]
        public Customer Customer;

        [DataMember(Name = "balance")]
        public int Balance;

        [DataMember(Name = "points")]
        public int Points;

        [DataMember(Name = "last_usage")]
        public string FormattedLastUsage
        {
            get { return LastUsage.ToDateTimeZone(); }
            set { LastUsage = value.ToDateTime(); }
        }    
        public DateTime? LastUsage;

        [DataMember(Name = "last_charge")]
        public string FormattedLastCharge
        {
            get { return LastCharge.ToDateTimeZone(); }
            set { LastCharge = value.ToDateTime(); }
        }         
        public DateTime? LastCharge;

        [DataMember(Name = "stock_status")]
        public string StockStatus;

        [DataMember(Name = "lock_status")]
        public string LockStatus;
    }
}
