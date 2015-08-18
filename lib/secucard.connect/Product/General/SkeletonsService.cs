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

namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.General.Model;

    public class SkeletonsService : ProductService<Skeleton>
    {
        public static readonly ServiceMetaData<Skeleton> META_DATA = new ServiceMetaData<Skeleton>(
            "general", "skeletons");

        protected override ServiceMetaData<Skeleton> GetMetaData()
        {
            return META_DATA;
        }

        public void CreateEvent()
        {
            ExecuteToBool("12345", "Demoevent", null,
                new Demoevent
                {
                    Delay = 2,
                    Target = "general.skeletons",
                    Type = "DemoEvent",
                    Data = "{ whatever: \"whole object gets send as payload for event\"}"
                }, new ChannelOptions {Channel = ChannelOptions.CHANNEL_STOMP, TimeOutSec = 100});
        }
    }
}