namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Attachment : MediaResource
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}