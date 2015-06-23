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
        public Customer customer;

        [DataMember(Name = "contract")]
        public Contract contract;

        [DataMember(Name = "amount")]
        public long amount;

        //TODO:
        //public Currency currency;

        [DataMember(Name = "purpose")]
        public string purpose;

        [DataMember(Name = "orderId")]
        public string orderId;

        [DataMember(Name = "transId")]
        public string transId;

        [DataMember(Name = "status")]
        public string status;

        [DataMember(Name = "transaction_status")]
        public string transactionStatus;
    }
}
