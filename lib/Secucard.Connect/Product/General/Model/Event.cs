namespace Secucard.Connect.Product.General.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Event<T> : SecuObject
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "target")]
        public string Target { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }

        public override string ToString()
        {
            return "Event{" +
                   "type='" + Type + '\'' +
                   ", target='" + Target + '\'' +
                   ", created=" + Created +
                   ", data=" + Data +
                   '}';
        }
    }
}