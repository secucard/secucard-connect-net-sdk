namespace Secucard.Model.General
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General.Components;

    [DataContract]
    public class AccountDevice : SecuObject
    {
        public bool Checkin;

        public override string ServiceResourceName
        {
            get { return "general.accountdevices"; }
        }

        [DataMember(Name = "platform")]
        public string Platform { get; set; }

        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "info")]
        public List<NameValueItem> Info { get; set; }

        [DataMember(Name = "online")]
        public bool Online { get; set; }
    }
}