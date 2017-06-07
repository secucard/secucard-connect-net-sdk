namespace Secucard.Connect.Product.Smart.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReceiptLineValue
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "decoration")]
        public List<string> Decoration { get; set; }
    }
}