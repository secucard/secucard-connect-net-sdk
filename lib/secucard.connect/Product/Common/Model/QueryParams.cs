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