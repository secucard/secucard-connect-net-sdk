namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class StompResult
    {
        [DataMember(Name = "result")]
        public bool? Result { get; set; }
    }
}