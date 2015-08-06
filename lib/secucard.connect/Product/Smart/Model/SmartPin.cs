namespace Secucard.Connect.Product.Smart.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

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