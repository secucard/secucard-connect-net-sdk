namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayInvoice : Transaction
    {
        public override string ToString()
        {
            return "SecupayInvoice{ " + base.ToString() + " }";
        }
    }
}