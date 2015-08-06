namespace Secucard.Connect.Product.General.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Assignment
    {
        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "owner")]
        public bool IsOwner { get; set; }

        [DataMember(Name = "assign")]
        public Assign Assign { get; set; }
    }
}