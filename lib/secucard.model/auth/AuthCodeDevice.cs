namespace secucard.model.auth
{
    using System.Runtime.Serialization;

    [DataContract]
    public class AuthCodeDevice
    {
        [DataMember(Name = "device_code")]
        public string DeviceCode { get; set; }

        [DataMember(Name = "user_code")]
        public string UserCode { get; set; }

        [DataMember(Name = "verification_url")]
        public string VerificationUrl { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "interval")]
        public int Interval { get; set; }

        public override string ToString()
        {
            return "DeviceAuthCode {" +
                "deviceCode='" + DeviceCode + '\'' +
                ", userCode='" + UserCode + '\'' +
                ", verificationUrl='" + VerificationUrl + '\'' +
                ", expiresIn=" + ExpiresIn +
                ", interval=" + Interval +
                '}';
        }
    }
}
