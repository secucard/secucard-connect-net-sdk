namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CrowdFundingDataProject
    {
        [DataMember(Name = "total_amount")]
        public int TotalAmount { get; set; }

        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }

        [DataMember(Name = "debit")]
        public CrowdFundingDataProjectAmount Debit { get; set; }

        [DataMember(Name = "credit_card")]
        public CrowdFundingDataProjectAmount CreditCard { get; set; }

        [DataMember(Name = "prepay")]
        public CrowdFundingDataProjectAmount Prepay { get; set; }

        [DataMember(Name = "sofort")]
        public CrowdFundingDataProjectAmount Sofort { get; set; }

        public override string ToString()
        {
            return "CrowdFundingDataProject{" +
                   "total_amount='" + this.TotalAmount + '\'' +
                   ", total_count='" + this.TotalCount + '\'' +
                   ", debit='" + this.Debit + '\'' +
                   ", credit_card='" + this.CreditCard + '\'' +
                   ", prepay='" + this.Prepay + '\'' +
                   ", sofort='" + this.Sofort + '\'' +
                   "} " + base.ToString();
        }
    }
}