namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class AssignExternalInvoicePdfRequest
    {
        [DataMember(Name = "update_existing")]
        public bool updateExisting { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return "AssignExternalInvoicePdfRequest{" +
                   "updateExisting='" + (this.updateExisting ? 1 : 0) + '\'' +
                   "Name='" + this.Name + '\'' +
                   '}';
        }
    }
}