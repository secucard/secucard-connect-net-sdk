namespace Secucard.Model.Services.Idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Attachment : MediaResource
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}