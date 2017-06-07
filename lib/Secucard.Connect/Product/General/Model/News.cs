namespace Secucard.Connect.Product.General.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class News : SecuObject
    {
        [DataMember(Name = "_account_read")]
        public string AccountRead { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

        [DataMember(Name = "document_id")]
        public string DocumentId { get; set; }

        [DataMember(Name = "headline")]
        public string Headline { get; set; }

        [DataMember(Name = "related")]
        public List<Merchant> Related { get; set; }

        [DataMember(Name = "text_full")]
        public string TextFull { get; set; }

        [DataMember(Name = "text_teaser")]
        public string TextTeaser { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        private string _picture;

        [DataMember(Name = "picture")]
        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                PictureObject = MediaResource.Create(value);
            }
        }

        [IgnoreDataMember]
        public MediaResource PictureObject { get; set; }
    }
}