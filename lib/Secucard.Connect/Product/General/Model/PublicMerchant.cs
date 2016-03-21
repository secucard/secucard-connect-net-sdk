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
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class PublicMerchant : SecuObject
    {
        [DataMember(Name = "address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [DataMember(Name = "address_formatted")]
        public string AddressFormatted { get; set; }

        [DataMember(Name = "category")]
        public List<string> Category { get; set; }

        [DataMember(Name = "category_main")]
        public string CategoryMain { get; set; }

        [DataMember(Name = "_geometry")]
        public int Distance { get; set; }

        [DataMember(Name = "geometry")]
        public Geometry Geometry { get; set; }

        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "open_hours")]
        public List<OpenHours> OpenHours { get; set; }

        [DataMember(Name = "open_now")]
        public bool OpenNow { get; set; }

        [DataMember(Name = "open_time")]
        public int OpenTime { get; set; }

        [DataMember(Name = "phone_number_formatted")]
        public string PhoneNumberFormatted { get; set; }

        [DataMember(Name = "photo")]
        public List<string> Photo { get; set; }

        [DataMember(Name = "photo_main")]
        public string PhotoMain { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "url_googleplus")]
        public string UrlGooglePlus { get; set; }

        [DataMember(Name = "url_website")]
        public string UrlWebsite { get; set; }

        [DataMember(Name = "utc_offset")]
        public int UtcOffset { get; set; }
    }
}