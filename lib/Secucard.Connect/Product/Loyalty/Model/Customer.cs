namespace Secucard.Connect.Product.Loyalty.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;
    using System;
    using Net.Util;

    [DataContract]
    public class Customer : SecuObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "account_contact")]
        public Contact AccountContact { get; set; }

        [DataMember(Name = "merchant_contact")]
        public Contact MerchantContact { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "days_until_birthday")]
        public string DaysUntilBirthday { get; set; }

        [DataMember(Name = "additional_data")]
        public object AdditionalData { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "customernumber")]
        public string CustomerNumber { get; set; }
    }
}