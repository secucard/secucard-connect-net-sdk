namespace Secucard.Model.Loyalty
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Bonus
    {
        [DataMember(Name = "amount")]
        public int Amount;

        /**   * PTS or EUR   */
        [DataMember(Name = "currency")]
        public string Currency;

        [DataMember(Name = "balance")]
        public int Balance;
    }
}
