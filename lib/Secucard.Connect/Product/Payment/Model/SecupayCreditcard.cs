namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayCreditcard : Transaction
    {
        [DataMember(Name = "container")]
        public Container Container { get; set; }

        public override string ToString()
        {
            return "SecupayCreditcard{" +
                   "container=" + Container +
                   "} " + base.ToString();
        }
    }
}