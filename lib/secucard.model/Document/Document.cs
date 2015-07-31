namespace Secucard.Model.Document
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Document : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "document.uploads"; }
        }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }
    }
}