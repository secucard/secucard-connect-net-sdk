namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SmartPin : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "smart.pin"; }
        }

        [DataMember(Name = "user_pin")]
        public string UserPin { get; set; }
    }
}