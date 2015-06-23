namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class OpenHours
    {
        [DataMember(Name = "close")] public DayTime Close;
        [DataMember(Name = "open")] public DayTime Open;

        public override string ToString()
        {
            return string.Format("Open: {0}, Close: {1}", Open, Close);
        }
    }
}