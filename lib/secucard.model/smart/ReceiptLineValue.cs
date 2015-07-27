namespace Secucard.Model.Smart
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReceiptLineValue
    {
        [DataMember(Name = "text")]
        public string Text;

        [DataMember(Name = "caption")]
        public string Caption;

        [DataMember(Name = "decoration")]
        public List<string> Decoration;
    }
}
