namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Contract : SecuObject
    {
        [System.Obsolete("was removed from api")]
        [DataMember(Name = "allow_cloning")]
        public bool? AllowCloning { get; set; }

        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "contract_id")]
        public string ContractId { get; set; }

        [System.Obsolete("was removed from api")]
        [DataMember(Name = "internal_reference")]
        public string InternalReference { get; set; }

        [DataMember(Name = "parent")]
        public Contract Parent { get; set; }

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
    }
}