namespace Secucard.Connect.Product.Payment.Model
{
    using Common.Model;
    using System.Runtime.Serialization;

    [DataContract]
    public class OptData : SecuObject
    {
        [DataMember(Name = "has_accepted_disclaimer")]
        public bool DisclaimerAccepted { get; set; }

        [DataMember(Name = "hide_disclaimer")]
        public bool HideDisclaimer { get; set; }
    }
}
