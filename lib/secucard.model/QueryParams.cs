//@JsonInclude(JsonInclude.Include.NON_EMPTY)

namespace Secucard.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class QueryParams
    {
        //public static final String SORT_ASC = "asc";
        //public static final String SORT_DESC = "desc";

        [DataMember(Name = "count")]
        public int Count;

        [DataMember(Name = "offset")]
        public int Offset;

        [DataMember(Name = "scroll_id")]
        public string ScrollId;

        [DataMember(Name = "scroll_expire")]
        public string ScrollExpire;

        [DataMember(Name = "fields")]
        public List<string> Fields;

        [DataMember(Name = "sort")]
        public Dictionary<string, string> SortOrder;

        [DataMember(Name = "q")]
        public string Query;

        [DataMember(Name = "preset")]
        public string Preset;

        [DataMember(Name = "geo")]
        public GeoQuery GeoQueryObj;




        //public void setFields(String... fields) {
        //    if (this.fields == null) {
        //        this.fields = new ArrayList<>(fields.length);
        //    }
        //    this.fields.addAll(Arrays.asList(fields));
        //}


        //public void addSortOrder(string field, string order) {
        //    if (sortOrder == null) {
        //        sortOrder = new HashMap<>();
        //    }
        //    sortOrder.put(field, order);
        //}


        public class GeoQuery
        {
            public string Field;

            public string Distance;

            public double Lat;

            public double Lon;

            //public GeoQuery() {
            //}

            //public GeoQuery(string field, double lat, double lon, string distance) {
            //    this.field = field;
            //    this.distance = distance;
            //    this.lat = lat;
            //    this.lon = lon;
            //}

            //public GeoQuery(double lat, double lon, string distance) {
            //    this.distance = distance;
            //    this.lat = lat;
            //    this.lon = lon;
            //}
        }
    }
}
