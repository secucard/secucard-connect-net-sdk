namespace Secucard.Connect.Product.Loyalty.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Card : SecuObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "cardNumber")]
        public string CardNumber { get; set; }

        public DateTime? Created { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public override string ServiceResourceName
        {
            get { return "loyalty.cards"; }
        }
    }
}