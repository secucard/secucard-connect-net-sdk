using System.Runtime.Serialization;

namespace Secucard.Model.General
{
    using System;
    using System.Collections.Generic;

    public class News : SecuObject
    {

        public override string SecuObjectName
        {
            get { return "general.news"; }
        }

        //public static final String STATUS_READ = "read";
        //public static final String STATUS_UNREAD = "unread";

        [DataMember(Name = "headline")]
        public string Headline;

        [DataMember(Name = "text_teaser")]
        public string TextTeaser;

        [DataMember(Name = "text_full")]
        public string TextFull;

        [DataMember(Name = "author")]
        public string Author;

        [DataMember(Name = "document_id")]
        public string DocumentId;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created;

        [DataMember(Name = "headline")]
        public string Picture;

        [DataMember(Name = "_account_read")]
        public string AccountRead;

        public MediaResource PictureObject;

        //@JsonTypeInfo(use = JsonTypeInfo.Id.NAME, include = JsonTypeInfo.As.PROPERTY, property = SecuObject.OBJECT_PROPERTY)
        //@JsonSubTypes({
        //    @JsonSubTypes.Type(value = Merchant.class, name = Merchant.OBJECT)})

        [DataMember(Name = "related")]
        public List<SecuObject> Related;




        //public void setPicture(String value) {
        //  this.picture = value;
        //  pictureObject = MediaResource.create(picture);
        //}

    }
}
