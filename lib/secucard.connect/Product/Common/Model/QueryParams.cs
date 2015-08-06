namespace Secucard.Connect.Product.Common.Model
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    [DataContract]
    public class QueryParams
    {
        public static string SORT_ASC = "asc";
        public static string SORT_DESC = "desc";

        [DataMember(Name = "count")]
        public int? Count;

        [DataMember(Name = "offset")]
        public int? Offset;

        [DataMember(Name = "scroll_id")]
        public string ScrollId;

        [DataMember(Name = "scroll_expire")]
        public string ScrollExpire;

        [DataMember(Name = "fields")]
        public List<string> Fields;

        [DataMember(Name = "sort")]
        public NameValueCollection SortOrder;

        [DataMember(Name = "q")]
        public string Query;

        [DataMember(Name = "preset")]
        public string Preset;

        [DataMember(Name = "geo")]
        public GeoQuery GeoQueryObj;

        [DataMember(Name = "expand")]
        public bool? Expand;

        public class GeoQuery
        {
            public string Field;

            public string Distance;

            public double? Lat;

            public double? Lon;
        }
    }
}
