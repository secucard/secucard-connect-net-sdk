namespace Secucard.Model.General
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General.Components;

    [DataContract]
    public class PublicMerchant : SecuObject
    {
        [DataMember(Name = "address_components")] public List<AddressComponent> AddressComponents;
        [DataMember(Name = "address_formatted")] public string AddressFormatted;
        [DataMember(Name = "category")] public List<string> Category;
        [DataMember(Name = "category_main")] public string CategoryMain;
        public bool CheckedIn;
        [DataMember(Name = "_geometry")] public int Distance;
        [DataMember(Name = "geometry")] public Geometry Geometry;
        [DataMember(Name = "hash")] public string Hash;
        [DataMember(Name = "key")] public string Key;
        [DataMember(Name = "name")] public string Name;
        [DataMember(Name = "open_hours")] public List<OpenHours> OpenHours;
        [DataMember(Name = "open_now")] public bool OpenNow;
        [DataMember(Name = "open_time")] public int OpenTime;
        [DataMember(Name = "phone_number_formatted")] public string PhoneNumberFormatted;
        [DataMember(Name = "photo")] public List<string> Photo;
        [DataMember(Name = "photo_main")] public string PhotoMain;
        [DataMember(Name = "source")] public string Source;
        [DataMember(Name = "url_googleplus")] public string UrlGooglePlus;
        [DataMember(Name = "url_website")] public string UrlWebsite;
        [DataMember(Name = "utc_offset")] public int UtcOffset;

        public override string ServiceResourceName
        {
            get { return "general.publicmerchants"; }
        }
    }
}