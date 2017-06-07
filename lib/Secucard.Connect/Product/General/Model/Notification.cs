namespace Secucard.Connect.Product.General.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Notification : SecuObject
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        public override string ToString()
        {
            return "Notification{" +
                   "text='" + Text + '\'' +
                   "} " + base.ToString();
        }
    }
}