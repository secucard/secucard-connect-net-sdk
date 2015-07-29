namespace Secucard.Model.Smart
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;
    using Secucard.Model.Loyalty;

    [DataContract]
    public class Checkin : SecuObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        public DateTime? Created { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "customerName")]
        public string CustomerName { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        public MediaResource PictureObject { get; set; }

        public override string ServiceResourceName
        {
            get { return "smart.checkins"; }
        }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public override string ToString()
        {
            return "Checkin{" +
                   "customerName='" + CustomerName + '\'' +
                   ", pictureUrl='" + Picture + '\'' +
                   ", created=" + Created +
                   "} " + base.ToString();
        }
    }
}