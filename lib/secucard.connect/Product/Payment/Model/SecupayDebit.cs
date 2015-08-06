namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayDebit : Transaction
    {
        public override string ServiceResourceName
        {
            get { return "payment.secupaydebits"; }
        }

        [DataMember(Name = "container")]
        public Container Container { get; set; }

        public override string ToString()
        {
            return "SecupayDebit{" +
                   "container=" + Container +
                   "} " + base.ToString();
        }
    }
}