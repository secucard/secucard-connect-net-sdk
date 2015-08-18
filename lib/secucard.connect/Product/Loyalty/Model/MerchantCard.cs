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

namespace Secucard.Connect.Product.Loyalty.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class MerchantCard : SecuObject
    {
        public DateTime? LastCharge;
        public DateTime? LastUsage;

        [DataMember(Name = "balance")]
        public int Balance { get; set; }

        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [DataMember(Name = "cardgroup")]
        public CardGroup Cardgroup { get; set; }

        [DataMember(Name = "created_for_merchant")]
        public Merchant CreatedForMerchant { get; set; }

        [DataMember(Name = "created_for_store")]
        public Store CreatedForStore { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "is_base_card")]
        public bool IsBaseCard { get; set; }

        [DataMember(Name = "lock_status")]
        public string LockStatus { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "points")]
        public int Points { get; set; }

        [DataMember(Name = "stock_status")]
        public string StockStatus { get; set; }

        [DataMember(Name = "last_usage")]
        public string FormattedLastUsage
        {
            get { return LastUsage.ToDateTimeZone(); }
            set { LastUsage = value.ToDateTime(); }
        }

        [DataMember(Name = "last_charge")]
        public string FormattedLastCharge
        {
            get { return LastCharge.ToDateTimeZone(); }
            set { LastCharge = value.ToDateTime(); }
        }
    }
}