namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Person
    {
        [DataMember(Name = "identificationprocess")]
        public IdentificationProcess IdentificationProcess { get; set; }

        [DataMember(Name = "identificationdocument")]
        public IdentificationDocument IdentificationDocument { get; set; }

        [DataMember(Name = "customdata")]
        public CustomData CustomData { get; set; }

        [DataMember(Name = "contactdata")]
        public ContactData ContactData { get; set; }

        [DataMember(Name = "attachments")]
        public List<Attachment> Attachments { get; set; }

        [DataMember(Name = "userdata")]
        public UserData UserData { get; set; }
    }
}