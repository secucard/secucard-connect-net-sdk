namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayCreditcard : Transaction
    {
        public override string ToString()
        {
            return "SecupayCreditcard{ " + base.ToString() + " }";
        }
    }
}