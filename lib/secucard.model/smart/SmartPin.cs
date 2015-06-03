namespace secucard.model.smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SmartPin
    {
        [DataMember(Name = "user_pin")]
        public string UserPin { get; set; }
    }
}
