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
    using Secucard.Connect.Net.Util;

    [DataContract]
    public class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        public Response(string json)
        {
            // On return data contains an unknown object that will be treated as a string at first.
            // Workaround: MS json serializer does not have the option to convert object to string
            var dict = new JsonSplitter().CreateDictionary(json);
            if (dict.ContainsKey("status")) Status = dict["status"];
            if (dict.ContainsKey("data")) Data = dict["data"];
        }
    }
}