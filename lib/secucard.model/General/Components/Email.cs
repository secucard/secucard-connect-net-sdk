namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Email 
    {
        [DataMember(Name = "type")]
        public double Type;

        [DataMember(Name = "email_formatted")]
        public double EmailFormatted;
    }
}
