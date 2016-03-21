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

namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;

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