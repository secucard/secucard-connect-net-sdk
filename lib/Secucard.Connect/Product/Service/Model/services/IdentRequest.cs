namespace Secucard.Connect.Product.Service.Model.services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Service.Model.services.idrequest;

    [DataContract]
    public class IdentRequest : SecuObject
    {
        public static string TypePerson = "person";
        public static string TypeCompany = "company";
        public static string StatusRequested = "requested";
        public static string StatusOk = "ok";
        public static string StatusFailed = "failed";

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        [DataMember(Name = "contract")]
        public IdentContract Contract { get; set; }

        [DataMember(Name = "owner_transaction_id")]
        public string OwnerTransactionId { get; set; }

        [DataMember(Name = "person")]
        public List<Person> Persons { get; set; }

        [DataMember(Name = "provider")]
        public string Provider { get; set; }

        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        public override string ToString()
        {
            return "IdentRequest{" +
                   "type='" + Type + '\'' +
                   ", status='" + Status + '\'' +
                   ", owner='" + Owner + '\'' +
                   ", contract=" + Contract +
                   ", ownerTransactionId='" + OwnerTransactionId + '\'' +
                   ", persons=" + Persons +
                   ", created=" + Created +
                   "} " + base.ToString();
        }
    }
}