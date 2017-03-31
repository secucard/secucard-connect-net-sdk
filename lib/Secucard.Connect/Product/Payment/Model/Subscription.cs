namespace Secucard.Connect.Product.Payment.Model
{
    using Common.Model;
    using System.Runtime.Serialization;

    /// <summary>
    /// Subscription Data Model class
    /// </summary>
    [DataContract]
    public class Subscription : SecuObject
    {
        [DataMember(Name = "purpose")]
        public string Purpose { get; set; }
    }
}
