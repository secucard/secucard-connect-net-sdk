using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Secucard.Model.General;

namespace Secucard.Model.Loyalty
{
    [DataContract]
    public class Sale : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "loyalty.sales"; }
        }
        [DataMember(Name = "amount")]
        public int amount;

        [DataMember(Name = "last_change")]
        public DateTime lastChange;

        [DataMember(Name = "status")]
        public int status;

        [DataMember(Name = "description")]
        public string description;

        [DataMember(Name = "description_raw")]
        public string descriptionRaw;

        [DataMember(Name = "store")]
        public Store store;

        [DataMember(Name = "card")]
        public Card card;

        [DataMember(Name = "cardgroup")]
        public CardGroup cardgroup;

        [DataMember(Name = "merchantcard")]
        public MerchantCard merchantcard;

        [DataMember(Name = "balance_amount")]
        public int balanceAmount;

        [DataMember(Name = "balance_points")]
        public int balancePoints;

        //TODO:
        //public Currency currency;

        [DataMember(Name = "created_for_merchant")]
        public List<Bonus> bonus;


    }
}
