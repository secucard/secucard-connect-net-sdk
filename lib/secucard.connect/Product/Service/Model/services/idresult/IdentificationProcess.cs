namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class IdentificationProcess
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "identificationtime")]
        public string IdentificationTimeFormatted
        {
            get { return IdentificationTime.ToDateTimeZone(); }
            set { IdentificationTime = value.ToDateTime(); }
        }

        public DateTime? IdentificationTime { get; set; }

        [DataMember(Name = "transactionnumber")]
        public string TransactionNumber { get; set; }

        public override string ToString()
        {
            return "IdentificationProcess{" +
                   "status='" + Status + '\'' +
                   ", identificationTime=" + IdentificationTime +
                   ", transactionNumber='" + TransactionNumber + '\'' +
                   '}';
        }
    }
}