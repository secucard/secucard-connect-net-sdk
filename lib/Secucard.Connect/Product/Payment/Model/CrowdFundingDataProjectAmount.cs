namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CrowdFundingDataProjectAmount
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        public override string ToString()
        {
            return "CrowdFundingDataProjectAmount{" +
                   "count='" + this.Count + '\'' +
                   ", amount='" + this.Amount + '\'' +
                   "} " + base.ToString();
        }
    }
}