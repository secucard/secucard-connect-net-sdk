namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Customer : SecuObject
    {
        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return this.Created.ToDateTimeZone(); }
            set { this.Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return this.Updated.ToDateTimeZone(); }
            set { this.Updated = value.ToDateTime(); }
        }

        public DateTime? Updated { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        public override string ToString()
        {
            return "Customer{" +
                   ", contact=" + Contact +
                   ", created=" + this.Created +
                   ", updated=" + this.Updated +
                   "} " + base.ToString();
        }
    }
}
