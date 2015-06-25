namespace Secucard.Model.General
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General.Components;

    [DataContract]
    public class Merchant : SecuObject
    {
        public override string ServiceResourceName { get { return "general.merchants"; } }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "location")]
        public Location Location;

        [DataMember(Name = "metadata")]
        public MetaData Metadata;

        [DataMember(Name = "name")]
        public string Name;

        [DataMember(Name = "photo")]
        public List<string> Photo;

        [DataMember(Name = "photo_main")]
        public string PhotoMain;
    }
}