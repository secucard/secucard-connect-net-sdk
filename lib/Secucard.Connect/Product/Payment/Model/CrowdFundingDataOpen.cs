namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CrowdFundingDataOpen
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "outside_cancellation_period")]
        public CrowdFundingDataOpenDetail OutsideCancellationPeriod { get; set; }

        [DataMember(Name = "inside_cancellation_period")]
        public CrowdFundingDataOpenDetail InsideCancellationPeriod { get; set; }

        public override string ToString()
        {
            return "CrowdFundingDataOpen{" +
                   "total='" + this.Total + '\'' +
                   ", outside_cancellation_period='" + this.OutsideCancellationPeriod + '\'' +
                   ", inside_cancellation_period='" + this.InsideCancellationPeriod + '\'' +
                   "} " + base.ToString();
        }
    }
}