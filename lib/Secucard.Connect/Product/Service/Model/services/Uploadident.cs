namespace Secucard.Connect.Product.Service.Model.services
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Uploadident : SecuObject
    {
        // The payment transactions id
        [DataMember(Name = "payment_id")]
        public string PaymentId { get; set; }

        // The general merchant id
        [DataMember(Name = "merchant_id")]
        public string MerchantId { get; set; }

        // The general contract id
        [DataMember(Name = "contract_id")]
        public string ContractId { get; set; }

        // The apikey
        [DataMember(Name = "apikey")]
        public string Apikey { get; set; }

        // A list of the uploaded file ids
        [DataMember(Name = "documents")]
        public string[] Documents { get; set; }

        // The ID of the created service case which was created to validate the payer / investor.
        // (readonly)
        [DataMember(Name = "service_issue_id")]
        public string ServiceIssueId { get; set; }
    }
}