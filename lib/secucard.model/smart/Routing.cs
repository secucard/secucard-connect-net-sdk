namespace Secucard.Model.Smart
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class Routing : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "smart.rountings"; }
        }

        [DataMember(Name = "store")]
        public Store Store;

        [DataMember(Name = "description")]
        public string Description;

        [DataMember(Name = "assign")]
        public List<Device> Assign;
}
}
