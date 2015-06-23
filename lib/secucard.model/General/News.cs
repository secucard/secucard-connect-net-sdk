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
        public string textTeaser;

        [DataMember(Name = "text_full")]
        public string textFull;

        [DataMember(Name = "author")]
        public string author;

        [DataMember(Name = "document_id")]
        public string documentId;

        [DataMember(Name = "created")]
        public DateTime created;

        [DataMember(Name = "headline")]
        public string picture;

        [DataMember(Name = "_account_read")]
        public string accountRead;

        public MediaResource pictureObject;

        //@JsonTypeInfo(use = JsonTypeInfo.Id.NAME, include = JsonTypeInfo.As.PROPERTY, property = SecuObject.OBJECT_PROPERTY)
        //@JsonSubTypes({
        //    @JsonSubTypes.Type(value = Merchant.class, name = Merchant.OBJECT)})

        [DataMember(Name = "related")]
        public List<SecuObject> related;




        //public void setPicture(String value) {
        //  this.picture = value;
        //  pictureObject = MediaResource.create(picture);
        //}

    }
}
