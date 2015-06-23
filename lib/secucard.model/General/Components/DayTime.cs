namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class DayTime
    {
        [DataMember(Name = "day")] public int Day;

        [DataMember(Name = "time")] public string Time;

        public override string ToString()
        {
            return string.Format("Day: {0}, Time: {1}", Day, Time);
        }
    }
}