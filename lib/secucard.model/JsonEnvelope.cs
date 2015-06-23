namespace Secucard.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class JsonEnvelope<T>
    {

        [DataMember(Name = "count")]
        public int Count { get; set; }


        [DataMember(Name = "data")]
        public T Data;

        public override string ToString()
        {
            return "JsonEnvelope {count='" + Count + "', data.type='" + Data.GetType().Name + '}';
        }

    }
}