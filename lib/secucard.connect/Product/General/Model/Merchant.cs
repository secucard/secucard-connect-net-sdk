namespace Secucard.Connect.Product.General.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Merchant : SecuObject
    {
        public override string ServiceResourceName
        {
            get { return "general.merchants"; }
        }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "metadata")]
        public MetaData Metadata { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "photo")]
        public List<string> Photo { get; set; }

        [DataMember(Name = "photo_main")]
        public string PhotoMain { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}