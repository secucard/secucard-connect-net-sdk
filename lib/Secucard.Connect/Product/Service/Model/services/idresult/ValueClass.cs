namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ValueClass
    {
        public static string StatusNew = "NEW";
        public static string StatusMatch = "MATCH";

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "original")]
        public string Original { get; set; }

        public override string ToString()
        {
            return "Value{" +
                   "value='" + Value + '\'' +
                   ", status='" + Status + '\'' +
                   ", original='" + Original + '\'' +
                   '}';
        }
    }
}