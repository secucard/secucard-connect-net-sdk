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
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Loyalty.Model;

    [DataContract]
    public class Ident : SecuObject
    {
        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "length")]
        public int? Length { get; set; }

        [DataMember(Name = "prefix")]
        public string Prefix { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "valid")]
        public bool? Valid { get; set; }

        public override string ToString()
        {
            return "Ident {" +
                   "type='" + Type + '\'' +
                   ", name='" + Name + '\'' +
                   ", length=" + Length +
                   ", prefix='" + Prefix + '\'' +
                   ", value='" + Value + '\'' +
                   ", customer=" + Customer +
                   ", merchantCard=" + MerchantCard +
                   ", valid=" + Valid +
                   "} " + base.ToString();
        }
    }
}