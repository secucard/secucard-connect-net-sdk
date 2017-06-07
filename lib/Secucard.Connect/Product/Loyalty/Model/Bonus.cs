namespace Secucard.Connect.Product.Loyalty.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Bonus
    {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "balance")]
        public int Balance { get; set; }

        /**   * PTS or EUR   */

        [DataMember(Name = "currency")]
        public string Currency { get; set; }
    }
}