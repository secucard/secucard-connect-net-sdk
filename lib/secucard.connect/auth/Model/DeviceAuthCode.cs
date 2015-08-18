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
    using System.Runtime.Serialization;

    [DataContract]
    public class DeviceAuthCode
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