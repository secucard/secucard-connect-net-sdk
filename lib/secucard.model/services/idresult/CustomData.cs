namespace Secucard.Model.Services.Idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomData
    {
        [DataMember(Name = "custom1")]
        public string Custom1 { get; set; }

        [DataMember(Name = "custom2")]
        public string Custom2 { get; set; }

        [DataMember(Name = "custom3")]
        public string Custom3 { get; set; }

        [DataMember(Name = "custom4")]
        public string Custom4 { get; set; }

        [DataMember(Name = "custom5")]
        public string Custom5 { get; set; }

        public override string ToString()
        {
            return "CustomData{" +
                   "custom1='" + Custom1 + '\'' +
                   ", custom2='" + Custom2 + '\'' +
                   ", custom3='" + Custom3 + '\'' +
                   ", custom4='" + Custom4 + '\'' +
                   ", custom5='" + Custom5 + '\'' +
                   '}';
        }
    }
}