namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ResultClass : SecuObject
    {

        public override string ServiceResourceName
        {
            get { return "general.result"; }
        }

        [DataMember(Name = "result")]
        public bool? Result { get; set; }


    }
}