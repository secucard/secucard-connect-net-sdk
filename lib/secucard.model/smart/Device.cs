namespace Secucard.Model.Smart
{
    using System;
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

        [DataMember(Name = "store")]
        public Store Store;

        [DataMember(Name = "vendor")]
        public string Vendor;

        [DataMember(Name = "vendor_uid")]
        public string VendorUid;

        [DataMember(Name = "device")]
        public General.Device GenralDevice;

        [DataMember(Name = "user_pin")]
        public string UserPin;

        [DataMember(Name = "description")]
        public string Description;


        [DataMember(Name = "online")]
        public bool Online;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created;

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
