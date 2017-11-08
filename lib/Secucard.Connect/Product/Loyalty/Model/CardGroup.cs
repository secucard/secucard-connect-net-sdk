namespace Secucard.Connect.Product.Loyalty.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class CardGroup : SecuObject
    {
        public const string TransactionTypeCharge = "charge";
        public const string TransactionTypeDischarge = "discharge";
        public const string TransactionTypeSaleRevenue = "sale_revenue";
        public const string TransactionTypeChargePoints = "charge_points";
        public const string TransactionTypeDischargePoints = "discharge_points";
        public const string TransactionTypeCashreport = "cashreport";

        private string _picture;

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "display_name_raw")]
        public string DisplayNameRaw { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "stock_warn_limit")]
        public int? StockWarnLimit { get; set; }

        [DataMember(Name = "picture")]
        public string Picture
        {
            get
            {
                return this._picture;
            }

            set
            {
                this._picture = value;
                this.PictureObject = MediaResource.Create(value);
            }
        }

        [IgnoreDataMember]
        public MediaResource PictureObject { get; set; }
    }
}