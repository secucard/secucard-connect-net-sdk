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

namespace Secucard.Connect.Auth.Model
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
        public string Id { get; set; }

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