namespace secucard.model.auth
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class AuthToken
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

        // UNIX timestamp of token expiring
        private DateTime? ExpireTime { get; set; }

        //public AuthToken(string accessToken, string refreshToken)
        //{
        //    this.AccessToken = accessToken;
        //    this.RefreshToken = refreshToken;
        //}


        //public void SetExpireTime()
        //{
        //    this.ExpireTime = DateTime.Now.AddMilliseconds(ExpiresIn * 1000);
        //}

        //public bool IsExpired()
        //{
        //    return ExpireTime.HasValue || DateTime.Now > ExpireTime;
        //}

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
