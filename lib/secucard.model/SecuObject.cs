namespace Secucard.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class SecuObject
    {
        // Name of properties of this object, avoids direct string usage.
        //public const string OBJECT_PROPERTY = "object";
        //public const string ID_PROPERTY = "id";

        public abstract string SecuObjectName { get; }
        private string ObjectName;
        [DataMember(Name = "object")]
        public virtual string Object { get { return SecuObjectName; } set { ObjectName = value; } }


        [DataMember(Name = "id")]
        public string Id;

        public override string ToString()
        {
            return "SecuObject{id='" + Id + "', object='" + Object + '}';
        }

    }
}

