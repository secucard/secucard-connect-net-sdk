namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class Transaction : SecuObject
    {
        //public static final String STATUS_ACCEPTED = "accepted";
        //public static final String STATUS_CANCELED = "canceled";
        //public static final String STATUS_PROCEED = "proceed";

        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        [DataMember(Name = "purpose")]
        public string Purpose { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "transaction_status")]
        public string TransactionStatus { get; set; }

        [DataMember(Name = "transId")]
        public string TransId { get; set; }
    }
}