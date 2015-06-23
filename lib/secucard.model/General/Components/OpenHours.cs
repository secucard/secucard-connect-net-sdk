namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class OpenHours
    {

        [DataMember(Name = "open")]
        public DayTime Open;

        [DataMember(Name = "close")]
        public DayTime Close;

    }
}