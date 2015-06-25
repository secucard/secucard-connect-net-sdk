namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayDebit : Transaction
    {

        [DataMember(Name = "container")]
        public Container container;


        public override string ServiceResourceName
        {
            get { return "payment.secupaydebits"; }
        }

        public override string ToString()
        {
            return "SecupayDebit{" +
                   "container=" + container +
                   "} " + base.ToString();
        }
    }
}
