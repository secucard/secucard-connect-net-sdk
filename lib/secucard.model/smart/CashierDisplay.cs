//@ProductInfo(resourceId = "cashierdisplay")
namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CashierDisplay {

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "device_id")]
        public string DeviceId;

        [DataMember(Name = "action")]
        public string Action;

        [DataMember(Name = "value")]
        public string Value;


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
