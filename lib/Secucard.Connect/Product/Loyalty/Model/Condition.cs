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
    using System.Runtime.Serialization;

    [DataContract]
    public class Condition
    {
        [DataMember(Name = "bonus")]
        public int Bonus { get; set; }

        [DataMember(Name = "bonus_type")]
        public string BonusType { get; set; }

        /**    * PTS or EUR    */

        [DataMember(Name = "curreny")]
        public string Currency { get; set; }

        // public static final String BONUS_TYPE_PERCENT = "percent";

        [DataMember(Name = "start_value")]
        public int StartValue { get; set; }
    }
}