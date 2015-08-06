namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class SecuObject
    {
        // Name of properties of this object, avoids direct string usage.
        //public const string OBJECT_PROPERTY = "object";
        //public const string ID_PROPERTY = "id";

        public abstract string ServiceResourceName { get; }
        private string _ServiceResourceName;
        [DataMember(Name = "object")]
        public virtual string Object { get { return ServiceResourceName; } set { _ServiceResourceName = value; } }


        [DataMember(Name = "id")]
        public string Id;

        public override string ToString()
        {
            return "SecuObject {id='" + Id + "', object='" + Object + '}';
        }
    }
}

