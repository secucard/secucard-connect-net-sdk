//@JsonInclude(JsonInclude.Include.NON_EMPTY)

namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// General message container used by stomp messages.
    /// </summary>
    [DataContract]
    public class Message
    {
        [DataMember(Name = "pid", EmitDefaultValue = false)]
        public string Pid { get; set; }

        [DataMember(Name = "sid", EmitDefaultValue = false)]
        public string Sid { get; set; }

        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

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