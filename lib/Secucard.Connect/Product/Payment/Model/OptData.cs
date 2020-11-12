namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Common.Model;

    [DataContract]
    public class OptData : SecuObject
    {
        [DataMember(Name = "has_accepted_disclaimer")]
        public bool DisclaimerAccepted { get; set; }

        [DataMember(Name = "hide_disclaimer")]
        public bool HideDisclaimer { get; set; }

        [DataMember(Name = "languange")]
        public string Language { get; set; }

        [DataMember(Name = "basket_title")]
        public string BasketTitle { get; set; }

        [DataMember(Name = "payment_hint_title")]
        public string PaymentHintTitle { get; set; }

        [DataMember(Name = "submit_button_title")]
        public string SubmitButtonTitle { get; set; }

        [DataMember(Name = "cancel_button_title")]
        public string CancelButtonTitle { get; set; }

        [DataMember(Name = "project_title")]
        public string ProjectTitle { get; set; }
    }
}
