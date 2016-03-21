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
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public abstract class Transaction : SecuObject
    {
        //public static final String STATUS_ACCEPTED = "accepted";
        //public static final String STATUS_CANCELED = "canceled";
        //public static final String STATUS_PROCEED = "proceed";

        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        [DataMember(Name = "purpose")]
        public string Purpose { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "transaction_status")]
        public string TransactionStatus { get; set; }

        [DataMember(Name = "transId")]
        public string TransId { get; set; }
    }
}