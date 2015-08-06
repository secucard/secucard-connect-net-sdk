namespace Secucard.Connect.Product.Service.Model.services.idrequest
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Person
    {
        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }

        [DataMember(Name = "redirect_url")]
        public string RedirectUrl { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "owner_transaction_id")]
        public string OwnerTransactionId { get; set; }

        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "custom1")]
        public string Custom1 { get; set; }

        [DataMember(Name = "custom2")]
        public string Custom2 { get; set; }

        [DataMember(Name = "custom3")]
        public string Custom3 { get; set; }

        [DataMember(Name = "custom4")]
        public string Custom4 { get; set; }

        [DataMember(Name = "custom5")]
        public string Custom5 { get; set; }

        public override string ToString()
        {
            return "Person{" +
                   "transactionId='" + TransactionId + '\'' +
                   ", redirectUrl='" + RedirectUrl + '\'' +
                   ", status='" + Status + '\'' +
                   ", ownerTransactionId='" + OwnerTransactionId + '\'' +
                   ", contact=" + Contact +
                   ", custom1='" + Custom1 + '\'' +
                   ", custom2='" + Custom2 + '\'' +
                   ", custom3='" + Custom3 + '\'' +
                   ", custom4='" + Custom4 + '\'' +
                   ", custom5='" + Custom5 + '\'' +
                   '}';
        }
    }
}