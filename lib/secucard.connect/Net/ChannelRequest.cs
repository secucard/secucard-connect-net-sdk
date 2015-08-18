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

namespace Secucard.Connect.Net
{
    using System.Collections.Generic;
    using Secucard.Connect.Product.Common.Model;

    public class ChannelRequest
    {
        public ChannelRequest()
        {
            ActionArgs = new List<string>();
        }

        public ChannelMethod Method { get; set; }
        public string Product { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public List<string> ActionArgs { get; set; }

        public string ObjectId { get; set; }
        public QueryParams QueryParams { get; set; }

        public object Object { get; set; }
        public List<SecuObject> Objects { get; set; }
        public string AppId { get; set; }

        public ChannelOptions ChannelOptions { get; set; }
    }
}