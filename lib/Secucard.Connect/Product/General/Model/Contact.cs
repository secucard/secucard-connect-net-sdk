namespace Secucard.Connect.Product.General.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using System.Collections.Generic;

    [DataContract]
    public class Contact : SecuObject
    {
        public const string GenderMale = "MALE";
        public const string GenderFemale = "FEMALE";

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
        public string Nationality { get; set; } // ISO 3166 country code like DE

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

        [DataMember(Name = "fax")]
        public string Fax { get; set; }

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        [DataMember(Name = "url_website")]
        public string UrlWebsite { get; set; }

        private string _picture;

        [DataMember(Name = "picture")]
        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                PictureObject = MediaResource.Create(value);
            }
        }

        [IgnoreDataMember]
        public MediaResource PictureObject { get; set; }

        [DataMember(Name = "ident_service_ids")]
        public List<string> IdentServiceIds { get; set; }

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
                   ", fax='" + Fax + '\'' +
                   ", nationality='" + Nationality + '\'' +
                   ", address=" + Address +
                   ", urlWebsite='" + UrlWebsite + '\'' +
                   ", picture='" + Picture + '\'' +
                   "} " + base.ToString();
        }
    }
}