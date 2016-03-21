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

namespace Secucard.Connect.Net.Stomp
{
    using Secucard.Connect.Client.Config;

    public class StompConfig
    {
        public StompConfig(Properties properties)
        {
            Ssl = properties.Get("Stomp.Ssl", true);

            Host = properties.Get("Stomp.Host", "connect.secucard.com");
            VirtualHost = properties.Get("Stomp.VirtualHost");
            Port = properties.Get("Stomp.port", 61614);

            HeartbeatMs = properties.Get("Stomp.HeartbeatMs", 5000);

            ReplyTo = properties.Get("Stomp.ReplyTo", "/temp-queue/main");
            Destination = properties.Get("Stomp.Destination", "/exchange/connect.api/");

            ConnectionTimeoutSec = properties.Get("Stomp.ConnectionTimeoutSec", 10);
            MessageTimeoutSec = properties.Get("Stomp.MessageTimeoutSec", 30);
            MaxMessageAgeSec = properties.Get("Stomp.MaxMessageAgeSec", 30);

            RequestSENDReceipt = properties.Get("Stomp.RequestSENDReceipt", true);

            AcceptVersion = "1.2";
        }

        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public string Destination { get; set; }
        public string ReplyTo { get; set; }
        public bool Ssl { get; set; }
        public string AcceptVersion { get; set; }
        public int ConnectionTimeoutSec { get; set; }
        public int MessageTimeoutSec { get; set; }
        public int MaxMessageAgeSec { get; set; }
        public int HeartbeatMs { get; set; }
        public bool RequestSENDReceipt { get; set; }

        public override string ToString()
        {
            return "StompConfig [" +
                   "Host = " + Host + "," +
                   "Port = " + Port + "," +
                   "ReplyTo = " + ReplyTo + "," +
                   "Destination = " + Destination + "," +
                   "MaxMessageAgeSec = " + MaxMessageAgeSec + "," +
                   "MessageTimeoutSec = " + MessageTimeoutSec + "]";
        }
    }
}