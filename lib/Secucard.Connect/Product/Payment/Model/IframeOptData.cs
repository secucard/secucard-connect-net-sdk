namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class IframeOptData
    {
        public const string CessionFormal = "formal";
        public const string CessionPersonal = "personal";

        [DataMember(Name = "show_basket")]
        public bool ShowBasket { get; set; }

        [DataMember(Name = "basket_title")]
        public string BasketTitle { get; set; }

        [DataMember(Name = "submit_button_title")]
        public string SubmitButtonTitle { get; set; }

        [DataMember(Name = "logo_base64")]
        public string LogoBase64 { get; set; }

        [DataMember(Name = "cession")]
        public string Cession { get; set; }

        public override string ToString()
        {
            return "IframeOptData{" +
                   "show_basket='" + this.ShowBasket + '\'' +
                   ", basket_title='" + this.BasketTitle + '\'' +
                   ", submit_button_title='" + this.SubmitButtonTitle + '\'' +
                   ", logo_base64='" + this.LogoBase64 + '\'' +
                   ", cession='" + this.Cession + '\'' +
                   '}';
        }
    }
}