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
    public class TransferAccount
    {
        [DataMember(Name = "accountnumber")]
        public string AccountNumber { get; set; }

        [DataMember(Name = "account_owner")]
        public string AccountOwner { get; set; }

        [DataMember(Name = "bankcode")]
        public string BankCode { get; set; }

        [DataMember(Name = "bic")]
        public string Bic { get; set; }

        [DataMember(Name = "iban")]
        public string Iban { get; set; }

        public override string ToString()
        {
            return "TransferAccount{" +
                   "accountOwner='" + AccountOwner + '\'' +
                   ", accountNumber='" + AccountNumber + '\'' +
                   ", iban='" + Iban + '\'' +
                   ", bic='" + Bic + '\'' +
                   ", bankCode='" + BankCode + '\'' +
                   '}';
        }
    }
}