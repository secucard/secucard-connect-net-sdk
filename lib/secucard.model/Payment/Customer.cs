namespace Secucard.Model.Payment
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class Customer : SecuObject
    {
        [DataMember(Name = "merchant")]
        public Merchant Merchant;

        [DataMember(Name = "contact")]
        public Contact Contact;

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
        public DateTime? Created;

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return Updated.ToDateTimeZone(); }
            set { Updated = value.ToDateTime(); }
        }
        public DateTime? Updated;

        [DataMember(Name = "contract")]
        public Contract Contract;

        public override string ToString()
        {
            return "Customer{" +
                   "merchant=" + Merchant +
                   ", contact=" + Contact +
                   ", created=" + Created +
                   ", updated=" + Updated +
                   ", contract=" + Contract +
                   "} " + base.ToString();
        }

        public override string SecuObjectName
        {
            get { return "payment.customers"; }
        }
    }
}
