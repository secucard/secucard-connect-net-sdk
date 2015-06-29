namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Email 
    {
        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "email_formatted")]
        public string EmailFormatted;
    }
}
