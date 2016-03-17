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
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class Sale : SecuObject
    {
        public DateTime? Created;
        public DateTime? LastChange;

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "balance_amount")]
        public int BalanceAmount { get; set; }

        [DataMember(Name = "balance_points")]
        public int BalancePoints { get; set; }

        //TODO:
        //public Currency currency;

        [DataMember(Name = "created_for_merchant")]
        public List<Bonus> Bonus { get; set; }

        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [DataMember(Name = "cardgroup")]
        public CardGroup Cardgroup { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "description_raw")]
        public string DescriptionRaw { get; set; }

        [DataMember(Name = "merchantcard")]
        public MerchantCard MerchantCard { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "store")]
        public Store Store { get; set; }

        [DataMember(Name = "last_change")]
        public string FormattedLastChange
        {
            get { return LastChange.ToDateTimeZone(); }
            set { LastChange = value.ToDateTime(); }
        }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
    }
}