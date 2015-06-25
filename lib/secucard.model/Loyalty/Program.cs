namespace Secucard.Model.Loyalty
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Program : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "loyalty.program"; }
        }

        [DataMember(Name = "description")]
        public string Description;

        [DataMember(Name = "cardGroup")]
        public CardGroup CardGroup;

        [DataMember(Name = "conditions")]
        public List<Condition> conditions;
    }
}

