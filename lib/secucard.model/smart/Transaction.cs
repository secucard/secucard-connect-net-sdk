namespace Secucard.Model.Smart
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Transaction : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "smart.transactions"; }
        }

        //  @JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "device_source", EmitDefaultValue = false)]
        public Device DeviceSource { get; set; }

        [DataMember(Name = "basket_info")]
        public BasketInfo BasketInfo { get; set; }

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "target_device", EmitDefaultValue = false)]
        public Device TargetDevice { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return Updated.ToDateTimeZone(); }
            set { Updated = value.ToDateTime(); }
        }

        public DateTime? Updated { get; set; }

        [DataMember(Name = "idents")]
        public List<Ident> Idents { get; set; }

        [DataMember(Name = "basket")]
        public Basket Basket { get; set; }

        [DataMember(Name = "merchantRef")]
        public string MerchantRef { get; set; }

        [DataMember(Name = "transactionRef")]
        public string TransactionRef { get; set; }

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "payment_method", EmitDefaultValue = false)]
        public string PaymentMethod { get; set; }

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "receipt", EmitDefaultValue = false)]
        public List<ReceiptLine> ReceiptLines { get; set; }

        // @JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "payment_requested", EmitDefaultValue = false)]
        public string PaymentRequested { get; set; }

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "payment_executed", EmitDefaultValue = false)]
        public string PaymentExecuted { get; set; }

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "error", EmitDefaultValue = false)]
        public string Error { get; set; }

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "texts", EmitDefaultValue = false)]
        public List<string> Texts { get; set; }

        public override string ToString()
        {
            return "Transaction{" +
                   "basketInfo=" + BasketInfo +
                   ", deviceSource=" + DeviceSource +
                   ", targetDevice=" + TargetDevice +
                   ", status='" + Status + '\'' +
                   ", created=" + Created +
                   ", idents=" + Idents +
                   ", basket=" + Basket +
                   ", merchantRef='" + MerchantRef + '\'' +
                   ", transactionRef='" + TransactionRef + '\'' +
                   "} " + base.ToString();
        }
    }
}