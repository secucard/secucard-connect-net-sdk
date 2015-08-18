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

namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Status
    {
        [DataMember(Name = "status")] public string StatusProp;

        [DataMember(Name = "error")] public string Error;

        [DataMember(Name = "error_details")] public string ErrorDetails;

        [DataMember(Name = "error_description")] public string ErrorDescription;

        [DataMember(Name = "error_user")] public string ErrorUser;

        [DataMember(Name = "code")] public string Code;

        [DataMember(Name = "supportId")] public string SupportId;

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