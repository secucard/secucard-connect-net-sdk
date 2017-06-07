namespace Secucard.Connect.Product.General.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Loyalty.Model;

    [DataContract]
    public class Store : SecuObject
    {
        [DataMember(Name = "address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [DataMember(Name = "address_formatted")]
        public string AddressFormatted { get; set; }

        [DataMember(Name = "_balance")]
        public int Balance { get; set; }

        [DataMember(Name = "category")]
        public List<string> Category { get; set; }

        [DataMember(Name = "category_main")]
        public string CategoryMain { get; set; }

        [DataMember(Name = "_checkin_status")]
        public string CheckInStatus { get; set; }

        [DataMember(Name = "_geometry")]
        public int Distance { get; set; }

        [DataMember(Name = "facebook_id")]
        public string FacebookId { get; set; }

        [DataMember(Name = "geometry")]
        public Geometry Geometry { get; set; }

        [DataMember(Name = "has_beacon")]
        public bool HasBeacon { get; set; }

        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        [DataMember(Name = "_isDefault")]
        public bool IsDefault { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "name_raw")]
        public string NameRaw { get; set; }

        [DataMember(Name = "_news_status")]
        public string NewsStatus { get; set; }

        [DataMember(Name = "open_hours")]
        public List<OpenHours> OpenHours { get; set; }

        [DataMember(Name = "open_now")]
        public bool OpenNow { get; set; }

        [DataMember(Name = "open_time")]
        public int OpenTime { get; set; }

        [DataMember(Name = "phone_number_formatted")]
        public string PhoneNumberFormatted { get; set; }

        [DataMember(Name = "photo")]
        public List<string> PictureUrls { get; set; }

        [DataMember(Name = "_points")]
        public int Points { get; set; }

        [DataMember(Name = "_program")]
        public Program Program { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "url_website")]
        public string UrlWebsite { get; set; }

        [DataMember(Name = "_news")]
        public List<News> News { get; set; }

        private string _logoUrl;

        [DataMember(Name = "photo_main")]
        public string LogoUrl
        {
            get { return _logoUrl; }
            set
            {
                _logoUrl = value;
                Logo = MediaResource.Create(value);
            }
        }

        [IgnoreDataMember]
        public MediaResource Logo { get; set; }
    }
}