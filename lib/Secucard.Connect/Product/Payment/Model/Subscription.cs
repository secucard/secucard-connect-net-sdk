namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Common.Model;

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
