namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ResultClass : SecuObject
    {
        [DataMember(Name = "result")]
        public bool? Result;


        public override string ServiceResourceName
        {
            get { return "general.result"; }
        }
    }
}
