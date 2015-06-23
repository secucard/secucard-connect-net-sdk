using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Secucard.Model.Loyalty
{
    [DataContract]
    public class Program : SecuObject
    {
        public override string SecuObjectName
        {
            get { throw new NotImplementedException(); }
        }

        [DataMember(Name = "description")]
        public string description;


        [DataMember(Name = "cardGroup")]
        public CardGroup cardGroup;

        [DataMember(Name = "conditions")]
        public List<Condition> conditions;
    }
}

