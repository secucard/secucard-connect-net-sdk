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
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
            Texts = new List<Text>();
        }

        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }

        [DataMember(Name = "texts")]
        public List<Text> Texts { get; set; }

        public override string ToString()
        {
            return "Basket{" +
                   "products=" + Products +
                   ", texts=" + Texts +
                   '}';
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void AddProduct(Text text)
        {
            Texts.Add(text);
        }
    }
}