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
namespace Secucard.Connect.Net.Stomp.Client
{
    public class StompConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Destination { get; set; }
        public string ReplyTo { get; set; }
        public bool Ssl { get; set; }

        public string AcceptVersion { get; set; }
        public string Login { get; set; } // TODO: move 
        public string Password { get; set; }
        public int SocketTimeoutSec { get; set; }
        public int ReceiptTimeoutSec { get; set; }
        public int ConnectionTimeoutSec { get; set; }
        public int MessageTimeoutSec { get; set; }
        public int MaxMessageAgeSec { get; set; }
        public bool DisconnectOnError { get; set; }
        public int HeartbeatClientMs { get; set; }
        public int HeartbeatServerMs { get; set; }
        public bool RequestDISCONNECTReceipt { get; set; }
        public int DisconnectOnSENDReceiptTimeout { get; set; }
        public bool RequestSENDReceipt { get; set; }
    }
}