namespace Secucard.Connect.Product.General.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class App : SecuObject
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}