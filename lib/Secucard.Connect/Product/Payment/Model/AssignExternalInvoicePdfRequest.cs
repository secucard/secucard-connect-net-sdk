namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class AssignExternalInvoicePdfRequest
    {
        [DataMember(Name = "update_existing")]
        public bool updateExisting { get; set; }

        public override string ToString()
        {
            return "AssignExternalInvoicePdfRequest{" +
                   "update_existing='" + (this.updateExisting ? 1 : 0) + '\'' +
                   '}';
        }
    }
}