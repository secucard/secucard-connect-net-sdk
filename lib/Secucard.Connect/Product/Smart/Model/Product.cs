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

namespace Secucard.Connect.Product.Smart.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Product
    {
        public Product()
        {
            Groups = new List<ProductGroup>();
        }

        [DataMember(Name = "tax")]
        public int? Tax { get; set; }

        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "parent", EmitDefaultValue = false)]
        public int? ParentId { get; set; }

        [DataMember(Name = "articleNumber")]
        public string ArticleNumber { get; set; }

        [DataMember(Name = "ean")]
        public string Ean { get; set; }

        [DataMember(Name = "desc")]
        public string Desc { get; set; }

        [DataMember(Name = "quantity")]
        public decimal Quantity { get; set; }

        [DataMember(Name = "priceOne")]
        public int? PriceOne { get; set; }

        [DataMember(Name = "group")]
        public List<ProductGroup> Groups { get; set; }

        public override string ToString()
        {
            return "Product{" +
                   "id='" + Id + '\'' +
                   ", parent='" + ParentId + '\'' +
                   ", articleNumber='" + ArticleNumber + '\'' +
                   ", ean='" + Ean + '\'' +
                   ", desc='" + Desc + '\'' +
                   ", quantity=" + Quantity +
                   ", priceOne=" + PriceOne +
                   ", tax=" + Tax +
                   ", productGroups=" + Groups +
                   '}';
        }
    }
}