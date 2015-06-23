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
        public Merchant merchant;

        [DataMember(Name = "created_for_merchant")]
        public Merchant createdForMerchant;

        [DataMember(Name = "card")]
        public Card card;

        [DataMember(Name = "created_for_store")]
        public Store createdForStore;

        [DataMember(Name = "is_base_card")]
        public bool isBaseCard;

        [DataMember(Name = "cardgroup")]
        public CardGroup cardgroup;

        [DataMember(Name = "customer")]
        public Customer customer;

        [DataMember(Name = "balance")]
        public int balance;

        [DataMember(Name = "points")]
        public int points;

        [DataMember(Name = "last_usage")]
        public DateTime lastUsage;

        [DataMember(Name = "last_charge")]
        public DateTime lastCharge;

        [DataMember(Name = "stock_status")]
        public string stockStatus;

        [DataMember(Name = "lock_status")]
        public string lockStatus;
    }
}
