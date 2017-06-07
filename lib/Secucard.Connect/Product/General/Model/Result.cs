namespace Secucard.Connect.Product.General.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class ResultClass : SecuObject
    {
        [DataMember(Name = "result")]
        public bool? Result { get; set; }
    }
}