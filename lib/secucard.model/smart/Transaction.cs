namespace Secucard.Model.Smart
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Transaction : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "smart.transactions"; }
        }

        [DataMember(Name = "basket_info")]
        public BasketInfo BasketInfo;

        //  @JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "device_source")]
        public Device DeviceSource;

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "target_device")]
        public Device TargetDevice;

        [DataMember(Name = "status")]
        public string Status;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        } 
        public DateTime? Created;

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return Updated.ToDateTimeZone(); }
            set { Updated = value.ToDateTime(); }
        }
        public DateTime? Updated;

        [DataMember(Name = "idents")]
        public List<Ident> Idents;

        [DataMember(Name = "basket")]
        public Basket Basket;

        [DataMember(Name = "merchantRef")]
        public string MerchantRef;

        [DataMember(Name = "transactionRef")]
        public string TransactionRef;

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "payment_method")]
        public string PaymentMethod;

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "receipt")]
        public List<ReceiptLine> ReceiptLines;

        // @JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "payment_requested")]
        public string PaymentRequested;

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "payment_executed")]
        public string PaymentExecuted;

        //@JsonInclude(JsonInclude.Include.NON_NULL)
        [DataMember(Name = "error")]
        public string Error;



        //public Transaction() {
        //}

        //public Transaction(BasketInfo basketInfo, Basket basket, List<Ident> idents) {
        //    this.basketInfo = basketInfo;
        //    this.basket = basket;
        //    this.idents = idents;
        //}



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
