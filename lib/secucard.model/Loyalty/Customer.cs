namespace Secucard.Model.Loyalty
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Model.General;
    
    [DataContract]
    public class Customer : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "loyalty.customers"; }
        }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "forename")]
        public string ForeName { get; set; }

        [DataMember(Name = "surname")]
        public string SurName { get; set; }

        [DataMember(Name = "company")]
        public string Company { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "salutation")]
        public string Salutation { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "zipcode")]
        public string Zipcode { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "fax")]
        public string Fax { get; set; }

        [DataMember(Name = "mobile")]
        public string Mobile { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "age")]
        public string Age { get; set; }

        [DataMember(Name = "days_until_birthday")]
        public string DaysUntilBirthday { get; set; }

        [DataMember(Name = "additional_data")]
        public List<string> AdditionalData { get; set; }

        [DataMember(Name = "customernumber")]
        public string CustomerNumber { get; set; }

        [DataMember(Name = "dob")]
        public string FormattedDateOfBirth
        {
            get { return  DateOfBirth.ToDateTimeZone(); }
            set { DateOfBirth = value.ToDateTime(); }
        }
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }


        public MediaResource pictureObject;


        //public void setPicture(string value) {
        //  picture = value;
        //  pictureObject = MediaResource.create(picture);
        //}
    }
}