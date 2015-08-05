namespace Secucard.Connect.auth.Model
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class Token
    {
        // Example: {"access_token":"73r39s7dccb27e37vho0hecs54","expires_in":1200,"token_type":"bearer","scope":null,"refresh_token":"02799fd07d091eefe4260d4c855a8f345d64c39a"}

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        public DateTime? ExpireTime { get; set; }
        public DateTime? OrigExpireTime { get; set; }

        public void SetExpireTime()
        {
            ExpireTime = DateTime.Now.AddMilliseconds(ExpiresIn*1000);
            if (OrigExpireTime == null) OrigExpireTime = ExpireTime;
        }

        public bool IsExpired()
        {
            return !ExpireTime.HasValue || DateTime.Now > ExpireTime;
        }

        public override string ToString()
        {
            return "Token{" +
                   "accessToken='" + AccessToken + '\'' +
                   ", expiresIn=" + ExpiresIn +
                   ", tokenType='" + TokenType + '\'' +
                   ", scope='" + Scope + '\'' +
                   ", refreshToken='" + RefreshToken + '\'' +
                   ", expireTime=" + ExpireTime +
                   '}';
        }
    }
}