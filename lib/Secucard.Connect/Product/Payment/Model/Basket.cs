namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Common.Model;

    [DataContract]
    public class Basket : SecuObject
    {
        public const string ItemTypeArticle = "article";
        public const string ItemTypeShipping = "shipping";
        public const string ItemTypeDonation = "donation";
        public const string ItemTypeStakeholderPayment = "stakeholder_payment";
        
        [DataMember(Name = "ean")]
        public string Ean { get; set; }

        [DataMember(Name = "tax")]
        public int? Tax { get; set; }

        [DataMember(Name = "price")]
        public int? PriceOne { get; set; }

        [DataMember(Name = "article_number")]
        public string ArticleNumber { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "total")]
        public int? Total { get; set; }

        [DataMember(Name = "quantity")]
        public int? Quantity { get; set; }

        [DataMember(Name = "item_type")]
        public string ItemType { get; set; }

        [DataMember(Name = "contract_id")]
        public string ContractId { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }
        
        [DataMember(Name = "apikey")]
        public string ApiKey { get; set; }
    }
}
