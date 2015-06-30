namespace Secucard.Model.Payment
{
    using System;
    using Secucard.Model.General;
    using System.Runtime.Serialization;

    [DataContract]
    public class Contract : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "payment.contracts"; }
        }

        [DataMember(Name = "contract_id")]
        public string ContractId;

        [DataMember(Name = "merchant")]
        public Merchant Merchant;

        [DataMember(Name = "internal_reference")]
        public string InternalReference;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created { get; set; }

        [DataMember(Name = "demo")]
        public bool? Demo;

        [DataMember(Name = "allow_cloning")]
        public bool? AllowCloning;
    }
}