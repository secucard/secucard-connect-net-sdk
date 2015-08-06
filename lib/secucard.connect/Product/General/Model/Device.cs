namespace Secucard.Connect.Product.General.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Device : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "general.devices"; }
        }
    }
}