namespace Secucard.Model.Smart
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class Routing : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "smart.routings"; }
        }

        [DataMember(Name = "store")]
        public Store Store { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "assign")]
        public List<Device> Assign { get; set; }
    }
}