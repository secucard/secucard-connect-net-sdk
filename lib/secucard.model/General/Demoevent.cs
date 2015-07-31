namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Demoevent
    {
        [DataMember(Name = "data")]
        public string Data { get; set; }

        [DataMember(Name = "delay")]
        public int Delay { get; set; }

        [DataMember(Name = "target")]
        public string Target { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}