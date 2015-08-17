/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Product.General.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Contact : SecuObject
    {
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