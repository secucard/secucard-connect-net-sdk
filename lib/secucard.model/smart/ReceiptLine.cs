namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ReceiptLine
    {
        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "value")]
        public string Value;

        public string toString()
        {
            return "ReceiptLine{" +
                   "type='" + Type + '\'' +
                   ", value='" + Value + '\'' +
                   '}';
        }
    }
}
