namespace Secucard.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ObjectList<T>
    {
        [DataMember(Name = "scroll_id")]
        private string ScrollId;

        [DataMember(Name = "count")]
        public int Count { get; set; }


        [DataMember(Name = "data")]
        public List<T> List; //List

        public override string ToString()
        {
            return "ObjectList{" + "scrollId='" + ScrollId + '\'' + ", count=" + Count + ", list=" + List + '}';
        }
    }
}