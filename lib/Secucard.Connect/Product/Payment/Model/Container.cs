namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Container : SecuObject
    {
        public const string TypeBankAccount = "bank_account";

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }
        
        // Deprecated: was not processed
        public Merchant Merchant { get; set; }

        [DataMember(Name = "private")]
        public Data PrivateData { get; set; }

        [DataMember(Name = "public")]
        public Data PublicData { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

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

        public override string ToString()
        {
            return "Container{" +
                   ", privateData=" + this.PrivateData +
                   ", publicData=" + this.PublicData +
                   ", type='" + this.Type + '\'' +
                   ", created=" + this.Created +
                   ", updated=" + this.Updated +
                   '}';
        }
    }
}