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

namespace Secucard.Connect.Product.Common.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class QueryParams
    {
        public static string SORT_ASC = "asc";
        public static string SORT_DESC = "desc";

        [DataMember(Name = "count", EmitDefaultValue = false)]
        public int? Count { get; set; }

        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public int? Offset { get; set; }

        [DataMember(Name = "scroll_id", EmitDefaultValue = false)]
        public string ScrollId { get; set; }

        [DataMember(Name = "scroll_expire", EmitDefaultValue = false)]
        public string ScrollExpire { get; set; }

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public List<string> Fields { get; set; }

        [DataMember(Name = "sort", EmitDefaultValue = false)]
        public Dictionary<string, string> SortOrder { get; set; }

        [DataMember(Name = "q", EmitDefaultValue = false)]
        public string Query { get; set; }

        [DataMember(Name = "preset", EmitDefaultValue = false)]
        public string Preset { get; set; }

        [DataMember(Name = "geo", EmitDefaultValue = false)]
        public GeoQuery GeoQueryObj { get; set; }

        [DataMember(Name = "expand", EmitDefaultValue = false)]
        public bool? Expand { get; set; }

        public class GeoQuery
        {
            public string Field { get; set; }
            public string Distance { get; set; }
            public double? Lat { get; set; }
            public double? Lon { get; set; }
        }
    }
}