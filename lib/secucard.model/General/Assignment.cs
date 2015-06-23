
namespace Secucard.Model.General
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General.Components;

    [DataContract]
    public class Assignment
    {
        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return  Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "owner")]
        public bool IsOwner { get; set; }

        [DataMember(Name = "assign")]
        public Assign Assign { get; set; }


        //@JsonTypeInfo(use = JsonTypeInfo.Id.NAME, include = JsonTypeInfo.As.PROPERTY,
        //        property = SecuObject.OBJECT_PROPERTY)
        //@JsonSubTypes({
        //        @JsonSubTypes.Type(value = Merchant.class, name = Merchant.OBJECT),
        //        @JsonSubTypes.Type(value = AccountDevice.class, name = AccountDevice.OBJECT),
        //        @JsonSubTypes.Type(value = Card.class, name = Card.OBJECT)})

    }
}
