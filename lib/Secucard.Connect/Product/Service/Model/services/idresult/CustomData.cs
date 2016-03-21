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
    public class CustomData
    {
        [DataMember(Name = "custom1")]
        public string Custom1 { get; set; }

        [DataMember(Name = "custom2")]
        public string Custom2 { get; set; }

        [DataMember(Name = "custom3")]
        public string Custom3 { get; set; }

        [DataMember(Name = "custom4")]
        public string Custom4 { get; set; }

        [DataMember(Name = "custom5")]
        public string Custom5 { get; set; }

        public override string ToString()
        {
            return "CustomData{" +
                   "custom1='" + Custom1 + '\'' +
                   ", custom2='" + Custom2 + '\'' +
                   ", custom3='" + Custom3 + '\'' +
                   ", custom4='" + Custom4 + '\'' +
                   ", custom5='" + Custom5 + '\'' +
                   '}';
        }
    }
}