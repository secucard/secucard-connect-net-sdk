namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Demoevent 
    {
        [DataMember(Name = "delay")]
        public int Delay;

        [DataMember(Name = "target")]
        public string Target;

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "data")]
        public string Data;

        //public override string ServiceResourceName
        //{
        //    get { return "general.demoevent"; }
        //}
    }
}
