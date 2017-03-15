/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
    }
}
