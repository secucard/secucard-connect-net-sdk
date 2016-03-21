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
    using System.Runtime.Serialization;

    [DataContract]
    public class IdentificationDocument
    {
        [DataMember(Name = "identificationprocess")]
        private ValueClass Country { get; set; }

        [DataMember(Name = "dateissued")]
        private ValueClass DateIssued { get; set; }

        [DataMember(Name = "issuedby")]
        private ValueClass IssuedBy { get; set; }

        [DataMember(Name = "number")]
        private ValueClass Number { get; set; }

        [DataMember(Name = "type")]
        private ValueClass Type { get; set; }

        [DataMember(Name = "validuntil")]
        private ValueClass ValidUntil { get; set; }

        public override string ToString()
        {
            return "IdentificationDocument{" +
                   "country=" + Country +
                   ", dateIssued=" + DateIssued +
                   ", issuedBy=" + IssuedBy +
                   ", number=" + Number +
                   ", type=" + Type +
                   ", validUntil=" + ValidUntil +
                   '}';
        }
    }
}