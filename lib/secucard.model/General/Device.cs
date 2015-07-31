namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Device : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "general.devices"; }
        }
    }
}