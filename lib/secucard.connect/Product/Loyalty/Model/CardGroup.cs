namespace Secucard.Connect.Product.Loyalty.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class CardGroup : SecuObject
    {
        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "display_name_raw")]
        public string DisplayNameRaw { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        public MediaResource pictureObject { get; set; }

        [DataMember(Name = "stock_warn_limit")]
        public int StockWarnLimit { get; set; }

        public override string ServiceResourceName
        {
            get { return "loyalty.cardgroups"; }
        }
    }
}