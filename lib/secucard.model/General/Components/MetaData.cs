namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MetaData : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "general.metadata"; }
        }
    }
}
