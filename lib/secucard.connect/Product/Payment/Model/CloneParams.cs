namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CloneParams
    {
        [DataMember(Name = "allow_transactions")]
        public bool? AllowTransactions { get; set; }

        [DataMember(Name = "payment_data")]
        public Data PaymentData { get; set; }

        [DataMember(Name = "project")]
        public string Project { get; set; }

        [DataMember(Name = "url_push")]
        public string PushUrl { get; set; }

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