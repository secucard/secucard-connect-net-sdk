//@ProductInfo(resourceId = "cashierdisplay")

namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CashierDisplay
    {
        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "device_id")]
        public string DeviceId { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return "CashierDisplay{" +
                   "deviceId='" + DeviceId + '\'' +
                   ", action='" + Action + '\'' +
                   ", value='" + Value + '\'' +
                   '}';
        }
    }
}