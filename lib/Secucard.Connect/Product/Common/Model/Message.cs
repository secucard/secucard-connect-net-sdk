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

//@JsonInclude(JsonInclude.Include.NON_EMPTY)

namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// General message container used by stomp messages.
    /// </summary>
    [DataContract]
    public class Message
    {
        [DataMember(Name = "pid", EmitDefaultValue = false)]
        public string Pid { get; set; }

        [DataMember(Name = "sid", EmitDefaultValue = false)]
        public string Sid { get; set; }

        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

        public override string ToString()
        {
            return "Message{" +
                   "pid='" + Pid + '\'' +
                   ", sid='" + Sid + '\'' +
                   ", query=" + Query +
                   ", data=" + Data +
                   "} " + base.ToString();
        }
    }
}