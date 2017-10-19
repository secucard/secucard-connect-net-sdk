namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Common.Model;

    /// <summary>
    /// Experience Data Model class
    /// </summary>
    [DataContract]
    public class Experience : SecuObject
    {
        /// <summary>
        /// The number of positive customer experiences
        /// </summary>
        [DataMember(Name = "positiv")]
        public int Positiv { get; set; }

        /// <summary>
        /// The number of negative customer experiences (open orders)
        /// </summary>
        [DataMember(Name = "negativ")]
        public int Negativ { get; set; }
    }
}
