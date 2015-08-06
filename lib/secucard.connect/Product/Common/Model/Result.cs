
/**
 * Generic result container used as payload (data) in stomp response messages.
 */
//@JsonInclude(JsonInclude.Include.NON_EMPTY)
namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Result
    {
        [DataMember(Name = "result")]
        public string ResultText { get; set; }

        [DataMember(Name = "request")]
        public string Request { get; set; }
    }
}
