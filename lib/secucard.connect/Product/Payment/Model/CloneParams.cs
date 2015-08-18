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

    [DataContract]
    public class CloneParams
    {
        [DataMember(Name = "allow_transactions")]
        public bool? AllowTransactions { get; set; }

        [DataMember(Name = "payment_data")]
        public Data PaymentData { get; set; }

        [DataMember(Name = "project")]
        public string Project { get; set; }

        [DataMember(Name = "url_push")]
        public string PushUrl { get; set; }

        public override string ToString()
        {
            return "CloneData{" +
                   "allowTransactions=" + AllowTransactions +
                   ", urlPush='" + PushUrl + '\'' +
                   ", paymentData=" + PaymentData +
                   ", project='" + Project + '\'' +
                   '}';
        }
    }
}