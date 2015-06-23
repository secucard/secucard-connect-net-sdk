namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Device : SecuObject
    {
        public override string SecuObjectName { get { return "general.devices"; } }

    }
}
