namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class PaymentTransaction : SecuObject
    {
        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "trans_id")]
        public int TransId { get; set; }

        [DataMember(Name = "product_id")]
        public int ProductId { get; set; }

        [DataMember(Name = "product")]
        public string Product { get; set; }

        [DataMember(Name = "product_raw")]
        public string ProductRaw { get; set; }

        [DataMember(Name = "zahlungsmittel_id")]
        public int ZahlungsmittelId { get; set; }

        [DataMember(Name = "contract_id")]
        public int ContractId { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return this.Created.ToDateTimeZone(); }
            set { this.Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return this.Updated.ToDateTimeZone(); }
            set { this.Updated = value.ToDateTime(); }
        }

        public DateTime? Updated { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "description_raw")]
        public string DescriptionRaw { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "status_text")]
        public string StatusText { get; set; }

        [DataMember(Name = "customer")]
        public User Customer { get; set; }

        public override string ToString()
        {
            return "PaymentTransaction{" +
                   "Merchant='" + this.Merchant + "', " +
                   "TransId='" + this.TransId + "', " +
                   "ProductId='" + this.ProductId + "', " +
                   "Product='" + this.Product + "', " +
                   "ProductRaw='" + this.ProductRaw + "', " +
                   "ZahlungsmittelId='" + this.ZahlungsmittelId + "', " +
                   "ContractId='" + this.ContractId + "', " +
                   "Amount='" + this.Amount + "', " +
                   "Currency='" + this.Currency + "', " +
                   "FormattedCreated='" + this.FormattedCreated + "', " +
                   "FormattedUpdated='" + this.FormattedUpdated + "', " +
                   "Description='" + this.Description + "', " +
                   "DescriptionRaw='" + this.DescriptionRaw + "', " +
                   "Status='" + this.Status + "', " +
                   "StatusText='" + this.StatusText + "', " +
                   "Customer='" + this.Customer + "'" +
                   "} " + base.ToString();
        }
    }
}