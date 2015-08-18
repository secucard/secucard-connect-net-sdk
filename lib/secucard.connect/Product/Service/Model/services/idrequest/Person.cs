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

namespace Secucard.Connect.Product.Service.Model.services.idrequest
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Person
    {
        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }

        [DataMember(Name = "redirect_url")]
        public string RedirectUrl { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "owner_transaction_id")]
        public string OwnerTransactionId { get; set; }

        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "custom1")]
        public string Custom1 { get; set; }

        [DataMember(Name = "custom2")]
        public string Custom2 { get; set; }

        [DataMember(Name = "custom3")]
        public string Custom3 { get; set; }

        [DataMember(Name = "custom4")]
        public string Custom4 { get; set; }

        [DataMember(Name = "custom5")]
        public string Custom5 { get; set; }

        public override string ToString()
        {
            return "Person{" +
                   "transactionId='" + TransactionId + '\'' +
                   ", redirectUrl='" + RedirectUrl + '\'' +
                   ", status='" + Status + '\'' +
                   ", ownerTransactionId='" + OwnerTransactionId + '\'' +
                   ", contact=" + Contact +
                   ", custom1='" + Custom1 + '\'' +
                   ", custom2='" + Custom2 + '\'' +
                   ", custom3='" + Custom3 + '\'' +
                   ", custom4='" + Custom4 + '\'' +
                   ", custom5='" + Custom5 + '\'' +
                   '}';
        }
    }
}