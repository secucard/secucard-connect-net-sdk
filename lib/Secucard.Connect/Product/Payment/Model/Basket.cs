namespace Secucard.Connect.Product.Payment.Model
{
    using Common.Model;
    using System.Runtime.Serialization;

    [DataContract]
    public class Basket : SecuObject
    {
        public const string itemTypeArticle = "article";
        public const string itemTypeShipping = "shipping";
        public const string itemTypeDonation = "donation";
        public const string itemTypeStakeholderPayment = "stakeholder_payment";


        [DataMember(Name = "ean")]
        public string Ean { get; set; }

        [DataMember(Name = "tax")]
        public int? Tax { get; set; }

        [DataMember(Name = "priceOne")]
        public int? PriceOne { get; set; }

        [DataMember(Name = "articleNumber")]
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
