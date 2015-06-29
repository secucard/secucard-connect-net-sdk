namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SmartPin: SecuObject
    {
        [DataMember(Name = "user_pin")]
        public string UserPin { get; set; }

        public override string ServiceResourceName
        {
            get { return "smart.pin"; }
        }
    }
}