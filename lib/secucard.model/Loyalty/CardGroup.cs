namespace Secucard.Model.Loyalty
{
    using System.Runtime.Serialization;
    using Secucard.Model.General;

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