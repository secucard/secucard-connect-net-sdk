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

namespace Secucard.Connect.Product.General.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class AddressComponent
    {
        [DataMember(Name = "long_name")]
        public string LongName { get; set; }

        [DataMember(Name = "short_name")]
        private string ShortName { get; set; }

        [DataMember(Name = "types")]
        private List<string> Types { get; set; }

        public override string ToString()
        {
            return string.Format("LongName: {0}, ShortName: {1}", LongName, ShortName);
        }
    }
}