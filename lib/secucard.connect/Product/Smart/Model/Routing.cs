namespace Secucard.Connect.Product.Smart.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

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