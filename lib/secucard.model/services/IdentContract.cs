namespace Secucard.Model.Services
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class IdentContract : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "services.contracts"; }
        }

        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "redirect_url_success")]
        public string RedirectUrlSuccess { get; set; }

        [DataMember(Name = "redirect_url_failed")]
        public string RedirectUrlFailed { get; set; }

        [DataMember(Name = "push_url")]
        public string PushUrl { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }
    }
}