namespace Secucard.Model.Loyalty
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Condition
    {
        // public static final String BONUS_TYPE_PERCENT = "percent";

        [DataMember(Name = "start_value")]
        public int StartValue;

        /**    * PTS or EUR    */
        [DataMember(Name = "curreny")]
        public string Currency;

        [DataMember(Name = "bonus")]
        public int Bonus;

        [DataMember(Name = "bonus_type")]
        public string BonusType;
    }
}