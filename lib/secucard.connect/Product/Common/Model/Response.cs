namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;

    [DataContract]
    public class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        public Response(string json)
        {
            // On return data contains an unknown object that will be treated as a string at first.
            // Workaround: MS json serializer does not have the option to convert object to string
            var dict = new JsonSplitter().CreateDictionary(json);
            Status = dict["status"];
            Data = dict["data"];
        }
    }
}
