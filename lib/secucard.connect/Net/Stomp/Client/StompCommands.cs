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

        public const string CONNECTED = "CONNECTED";
        public const string ERROR = "ERROR";
        public const string MESSAGE = "MESSAGE";
        public const string RECEIPT = "RECEIPT";
        // CLIENT
        public const string SEND = "SEND";
        public const string SUBSCRIBE = "SUBSCRIBE";
        public const string UNSUBSCRIBE = "UNSUBSCRIBE";
        public const string BEGIN = "BEGIN";
        public const string COMMIT = "COMMIT";
        public const string ABORT = "ABORT";
        public const string ACK = "ACK";
        public const string NACK = "NACK";
        public const string DISCONNECT = "DISCONNECT";
        public const string CONNECT = "CONNECT";
        public const string STOMP = "STOMP";
    }
}