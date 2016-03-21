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
    public static class StompCommands
    {
        // SERVER

        public const string Connected = "CONNECTED";
        public const string Error = "ERROR";
        public const string Message = "MESSAGE";
        public const string Receipt = "RECEIPT";
        // CLIENT
        public const string Send = "SEND";
        public const string Subscribe = "SUBSCRIBE";
        public const string Unsubscribe = "UNSUBSCRIBE";
        public const string Begin = "BEGIN";
        public const string Commit = "COMMIT";
        public const string Abort = "ABORT";
        public const string Ack = "ACK";
        public const string Nack = "NACK";
        public const string Disconnect = "DISCONNECT";
        public const string Connect = "CONNECT";
        public const string Stomp = "STOMP";
    }
}