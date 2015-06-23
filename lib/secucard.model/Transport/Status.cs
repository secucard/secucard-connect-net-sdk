//@JsonInclude(JsonInclude.Include.NON_EMPTY)

using System.Runtime.Serialization;

namespace Secucard.Model.Transport
{
    [DataContract]
    public class Status
    {
        [DataMember(Name = "status")]
        public string StatusProp;

        [DataMember(Name = "error")]
        public string Error;

        [DataMember(Name = "error_details")]
        public string ErrorDetails;

        [DataMember(Name = "error_description")]
        public string ErrorDescription;

        [DataMember(Name = "error_user")]
        public string ErrorUser;

        [DataMember(Name = "code")]
        public string Code;

        [DataMember(Name = "supportId")]
        public string SupportId;

        //public Status() {
        //}

        //public Status(string status, string error, string errorDetails) {
        //  this.status = status;
        //  this.error = error;
        //  this.errorDetails = errorDetails;
        //}

        public override string ToString()
        {
            return "Status{" +
                   "status='" + StatusProp + '\'' +
                   ", error='" + Error + '\'' +
                   ", errorDetails='" + ErrorDetails + '\'' +
                   ", errorDescription='" + ErrorDescription + '\'' +
                   ", errorUser='" + ErrorUser + '\'' +
                   ", code='" + Code + '\'' +
                   ", supportId='" + SupportId + '\'' +
                   '}';
        }
    }
}
