namespace Secucard.Model.Loyalty
{
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class Card : SecuObject
    {
        [DataMember(Name = "cardNumber")]
        public string CardNumber;

        [DataMember(Name = "created")]
        public string Created;

        [DataMember(Name = "account")]
        public Account Account;

        public override string SecuObjectName
        {
            get { return "loyalty.cards"; }
        }
    }
}