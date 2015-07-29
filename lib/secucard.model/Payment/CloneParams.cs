using System.Runtime.Serialization;

namespace Secucard.Model.Payment
{
    [DataContract]
    public class CloneParams
    {
        [DataMember(Name = "allow_transactions")]
        public bool? AllowTransactions;

        [DataMember(Name = "url_push")]
        public string PushUrl;

        [DataMember(Name = "payment_data")]
        public Data PaymentData;

        [DataMember(Name = "project")]
        public string Project;

        public override string ToString()
        {
            return "CloneData{" +
                   "allowTransactions=" + AllowTransactions +
                   ", urlPush='" + PushUrl + '\'' +
                   ", paymentData=" + PaymentData +
                   ", project='" + Project + '\'' +
                   '}';
        }
    }
}
