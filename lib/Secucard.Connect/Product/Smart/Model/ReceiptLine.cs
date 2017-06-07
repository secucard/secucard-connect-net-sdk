namespace Secucard.Connect.Product.Smart.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ReceiptLine
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "value")]
        public ReceiptLineValue Value { get; set; }

        public override string ToString()
        {
            return "ReceiptLine{" +
                   "type='" + Type + '\'' +
                   ", value='" + Value + '\'' +
                   '}';
        }
    }
}