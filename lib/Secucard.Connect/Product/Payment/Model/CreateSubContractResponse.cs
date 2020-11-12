namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class CreateSubContractResponse : SecuObject
    {
        [DataMember(Name = "apikey")]
        public string Apikey { get; set; }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

        [DataMember(Name = "payin_account")]
        public Data PayinAccount { get; set; }

        public override string ToString()
        {
            return "CreateSubContractResponse{" +
                   "apikey='" + this.Apikey + '\'' +
                   ", contract='" + Contract + '\'' +
                   ", payin_account='" + this.PayinAccount + '\'' +
                   "} " + base.ToString();
        }
    }
}