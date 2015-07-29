using System;

namespace Secucard.Model.General
{
    using System.Runtime.Serialization;
    using Secucard.Model.General.Components;

    [DataContract]
	public class Contact : SecuObject {

        public override string ServiceResourceName { get { return "general.contacts"; } }

        public const string GENDER_MALE = "MALE";
        public const string GENDER_FEMALE = "FEMALE";


        [DataMember(Name = "salutation")]
        public string Salutation { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "forename")]
        public string Forename { get; set; }

        [DataMember(Name = "surname")]
        public string Surname { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "nationality")]
        public string Nationality { get; set; }  // ISO 3166 country code like DE

        //@JsonFormat(shape= JsonFormat.Shape.STRING, pattern="yyyy-MM-dd")
        [DataMember(Name = "dob")]
        public string FormattedUpdated
        {
            get { return DateOfBirth.ToDateTimeZone(); }
            set { DateOfBirth = value.ToDateTime(); }
        }
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "birthplace")]
        public string BirthPlace { get; set; }

        [DataMember(Name = "companyname")]
        public string CompanyName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "mobile")]
        public string Mobile { get; set; }

        [DataMember(Name = "address")]
        public Address Address  { get; set; }

        [DataMember(Name = "url_website")]
        public string UrlWebsite { get; set; }

        [DataMember(Name = "picture")]
        private string Picture { get; set; }

        [IgnoreDataMemberAttribute]
        public MediaResource PictureObject { get; set; }


        


        /**
   * Setting the nationality in ISO 3166 2 letter code.
   * Case doesn't matter, will be corrected automatically.
   *
   * @param nationality The country code string.
   */
        //@JsonProperty
        //public void setNationality(string nationality) {
        //    Locale locale = LocaleUtil.toLocale(nationality, nationalityLocale);
        //    if (locale == null) {
        //        this.nationality = nationality;
        //    } else {
        //        setNationality(locale);
        //    }
        //}


        /**
   * Set the ISO nationality code by using a locale instance which is less error-prone then using a string
   */
       



        //public MediaResource getPictureObject() {
        //    return pictureObject;
        //}

        //public string getPicture() {
        //    return picture;
        //}

        //public void setPicture(string value) {
        //    this.picture = value;
        //    pictureObject = MediaResource.create(picture);
        //}

       



        
        public override string ToString()
        {
            return "Contact{" +
                   ", foreName='" + Forename + '\'' +
                   ", companyName='" + CompanyName + '\'' +
                   ", surName='" + Surname + '\'' +
                   ", title='" + Title + '\'' +
                   ", salutation='" + Salutation + '\'' +
                   ", gender='" + Gender + '\'' +
                   ", email='" + Email + '\'' +
                   ", dateOfBirth=" + DateOfBirth +
                   ", birthPlace='" + BirthPlace + '\'' +
                   ", phone='" + Phone + '\'' +
                   ", mobile='" + Mobile + '\'' +
                   ", nationality='" + Nationality + '\'' +
                   //", nationalityLocale=" + nationalityLocale +
                   ", address=" + Address +
                   ", urlWebsite='" + UrlWebsite + '\'' +
                   ", picture='" + Picture + '\'' +
                   "} " + base.ToString();
        }
	}
}
