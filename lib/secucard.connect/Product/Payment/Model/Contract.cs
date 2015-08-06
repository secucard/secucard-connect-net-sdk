namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Contract : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "payment.contracts"; }
        }

        [DataMember(Name = "allow_cloning")]
        public bool? AllowCloning { get; set; }

        [DataMember(Name = "contract_id")]
        public string ContractId { get; set; }

        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "internal_reference")]
        public string InternalReference { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }
    }
}