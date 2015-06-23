namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract] 
    public class DayTime  {

        [DataMember(Name = "day")]
        public int day;

        [DataMember(Name = "time")]
        public string time;
    }
}