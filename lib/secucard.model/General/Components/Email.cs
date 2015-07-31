namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Email
    {
        [DataMember(Name = "email_formatted")]
        public string EmailFormatted { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}