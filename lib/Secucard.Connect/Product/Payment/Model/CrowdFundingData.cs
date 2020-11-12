namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class CrowdFundingData : SecuObject
    {
        [DataMember(Name = "project")]
        public CrowdFundingDataProject Project { get; set; }

        [DataMember(Name = "paid_out")]
        public int PaidOut { get; set; }

        [DataMember(Name = "open")]
        public CrowdFundingDataOpen Open { get; set; }

        public override string ToString()
        {
            return "CrowdFundingData{" +
                   "project='" + this.Project + '\'' +
                   ", paid_out='" + this.PaidOut + '\'' +
                   ", open='" + this.Open + '\'' +
                   "} " + base.ToString();
        }
    }
}