namespace Secucard.Model.Payment
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;
    using Secucard.Model.General.Components;

    [DataContract]
	public class Contact {

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
        public string FormattedDateOfBirth
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

        [IgnoreDataMember]
        public MediaResource PictureObject { get; set; }

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
