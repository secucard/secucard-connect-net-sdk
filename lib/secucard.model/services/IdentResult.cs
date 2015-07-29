namespace Secucard.Model.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.Services.Idresult;

    [DataContract]
    public class IdentResult : SecuObject
    {
        public const string STATUS_OK = "ok";
        public static string STATUS_FAILED = "failed";
        public static string STATUS_PRELIMINARY_OK = "ok_preliminary";
        public static string STATUS_PRELIMINARY_FAILED = "failed_preliminary";

        public override string ServiceResourceName
        {
            get { return "services.identresults"; }
        }

        [DataMember(Name = "request")]
        public IdentRequest Request { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "person")]
        public List<Person> Persons { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "contract")]
        public IdentContract Contract { get; set; }

        public override string ToString()
        {
            return "IdentResult{" +
                   "request=" + Request +
                   ", status='" + Status + '\'' +
                   ", persons=" + Persons +
                   ", created=" + Created +
                   "} " + base.ToString();
        }
    }
}