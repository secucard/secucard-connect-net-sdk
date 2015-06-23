namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MetaData : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "general.metadata"; }
        }
    }
}
