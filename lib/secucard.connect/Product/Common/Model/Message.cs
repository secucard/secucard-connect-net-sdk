/**
 * General message container used by stomp messages.
 *
 * @param <T> The actual type of the payload data.
 */
//@JsonInclude(JsonInclude.Include.NON_EMPTY)

namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Message<T> : Status
    {
        [DataMember(Name = "pid")]
        public string Pid { get; set; }

        [DataMember(Name = "pid")]
        public string Sid { get; set; }

        [DataMember(Name = "pid")]
        public QueryParams Query { get; set; }

        [DataMember(Name = "pid")]
        public T Data { get; set; }

        public override string ToString()
        {
            return "Message{" +
                   "pid='" + Pid + '\'' +
                   ", sid='" + Sid + '\'' +
                   ", query=" + Query +
                   ", data=" + Data +
                   "} " + base.ToString();
        }
    }
}