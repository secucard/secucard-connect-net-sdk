namespace Secucard.Model.Smart
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;
    using Secucard.Model.Loyalty;

    [DataContract]
    public class Checkin : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "smart.checkins"; }
        }
        [DataMember(Name = "customerName")]
        public string CustomerName;

        [DataMember(Name = "picture")]
        public string Picture;


        public MediaResource pictureObject;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created;

        [DataMember(Name = "account")]
        public Account Account;

        [DataMember(Name = "customer")]
        public Customer Customer;

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
