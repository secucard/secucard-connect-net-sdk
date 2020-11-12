namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CrowdFundingDataOpenDetail
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        public override string ToString()
        {
            return "CrowdFundingDataOpenDetail{" +
                   "total='" + this.Total + '\'' +
                   "} " + base.ToString();
        }
    }
}