namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class Device : SecuObject
    {

        public override string SecuObjectName
        {
            get { return "smart.devices"; }
        }

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "merchant")]
        public Merchant Merchant;

        //public Device() {
        //}

        //public Device(String id) {
        //    this.id = id;
        //}

        //public Device(String id, String type) {
        //    this.id = id;
        //    this.type = type;
        //}

        public override string ToString()
        {
            return "Device{" +
                   "type='" + Type + '\'' +
                   '}';
        }

    }
}
