namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupaySofort : Transaction
    {
        public override string ToString()
        {
            return "SecupaySofort{ " + base.ToString() + " }";
        }
    }
}