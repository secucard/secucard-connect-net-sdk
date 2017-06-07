namespace Secucard.Connect.Product.Loyalty.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Condition
    {
        [DataMember(Name = "bonus")]
        public int Bonus { get; set; }

        [DataMember(Name = "bonus_type")]
        public string BonusType { get; set; }

        /**    * PTS or EUR    */

        [DataMember(Name = "curreny")]
        public string Currency { get; set; }

        // public static final String BONUS_TYPE_PERCENT = "percent";

        [DataMember(Name = "start_value")]
        public int StartValue { get; set; }
    }
}