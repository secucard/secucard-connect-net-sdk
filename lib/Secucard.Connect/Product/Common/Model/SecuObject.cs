namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecuObject
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "object")]
        public virtual string Object { get; set; }

        public override string ToString()
        {
            return "SecuObject {id='" + Id + "', object='" + Object + '}';
        }
    }
}