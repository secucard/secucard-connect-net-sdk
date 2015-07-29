namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class Transaction : SecuObject
    {
        //public static final String STATUS_ACCEPTED = "accepted";
        //public static final String STATUS_CANCELED = "canceled";
        //public static final String STATUS_PROCEED = "proceed";

        [DataMember(Name = "customer")]
        public Customer Customer;

        [DataMember(Name = "contract")]
        public Contract Contract;

        [DataMember(Name = "amount")]
        public long? Amount;

        [DataMember(Name = "currency")]
        public string Currency;

        [DataMember(Name = "purpose")]
        public string Purpose;

        [DataMember(Name = "orderId")]
        public string OrderId;

        [DataMember(Name = "transId")]
        public string TransId;

        [DataMember(Name = "status")]
        public string Status;

        [DataMember(Name = "transaction_status")]
        public string TransactionStatus;
    }
}
