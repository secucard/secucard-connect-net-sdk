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
    /// <summary>
    ///   Hold all options for the channel
    /// </summary>
    public class ChannelOptions
    {
        public const string CHANNEL_REST = "rest";
        public const string CHANNEL_STOMP = "stomp";
        public bool Anonymous = false;
        public bool Expand = false;
        public bool EventListening = false;
        public string Channel;
        public string ClientId = null;
        public int? TimeOutSec = null;

        /**
     * Set an callback to be executed after a resource was successfully retrieved.
     */
        // public Callback.Notify<?> resultProcessing;

        public static ChannelOptions GetDefault()
        {
            // TODO: Default Channel aus config
            return new ChannelOptions { Channel = CHANNEL_REST, TimeOutSec =100 }; 
        }

        public ChannelOptions()
        {
        }

        public ChannelOptions(string channel)
        {
            this.Channel = channel;
        }
    }
}
