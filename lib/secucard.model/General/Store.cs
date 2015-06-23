namespace Secucard.Model.General
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General.Components;
    using Secucard.Model.Loyalty;

    [DataContract]
    public class Store : SecuObject
    {

        //public static final String CHECKIN_STATUS_DECLINED_DISTANCE = "declined_distance";
        //public static final String CHECKIN_STATUS_DECLINED_NOTAVAIL = "declined_notavail";
        //public static final String CHECKIN_STATUS_AVAILABLE = "available";
        //public static final String CHECKIN_STATUS_CHECKED_IN = "checked_in";

        //public static final String NEWS_STATUS_READ = "read";
        //public static final String NEWS_STATUS_UNREAD = "unread";


        public override string SecuObjectName
        {
            get { return "general.stores"; }
        }

        [DataMember(Name = "source")]
        public string Source;

        [DataMember(Name = "key")]
        public string Key;

        [DataMember(Name = "hash")]
        public string Hash;

        [DataMember(Name = "name")]
        public string name;

        [DataMember(Name = "name_raw")]
        public string nameRaw;

        [DataMember(Name = "merchant")]
        public Merchant merchant;

        [DataMember(Name = "_news_status")]
        public string newsStatus;

        [DataMember(Name = "_news")]
        public List<News> news;

        [DataMember(Name = "open_now")]
        public bool openNow;

        [DataMember(Name = "open_time")]
        public int openTime;

        [DataMember(Name = "open_hours")]
        public List<OpenHours> openHours;

        [DataMember(Name = "geometry")]
        public Geometry geometry;

        [DataMember(Name = "_geometry")]
        public int distance;

        [DataMember(Name = "_checkin_status")]
        public string checkInStatus;

        [DataMember(Name = "address_formatted")]
        public string addressFormatted;

        [DataMember(Name = "address_components")]
        public List<AddressComponent> addressComponents;

        [DataMember(Name = "category")]
        public List<string> category;

        [DataMember(Name = "category_main")]
        public string categoryMain;

        [DataMember(Name = "phone_number_formatted")]
        public string phoneNumberFormatted;

        [DataMember(Name = "url_website")]
        public string urlWebsite;

        [DataMember(Name = "_balance")]
        public int balance;

        [DataMember(Name = "_points")]
        public int points;

        [DataMember(Name = "_program")]
        public Program program;

        [DataMember(Name = "_isDefault")]
        public bool isDefault;

        [DataMember(Name = "facebook_id")]
        public string facebookId;

        [DataMember(Name = "photo")]
        public List<string> pictureUrls;

        [DataMember(Name = "photo_main")]
        public string logoUrl;

        public MediaResource logo;

        [DataMember(Name = "has_beacon")]
        public bool hasBeacon;



    }
}
