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
    public static class StompHeader
    {
        public const string AcceptVersion = "accept-version";
        public const string Host = "host";
        public const string ContentType = "content-type";
        public const string ContentLength = "content-length";
        public const string Destination = "destination";
        public const string Receipt = "receipt";
        public const string ReceiptId = "receipt-id";
        public const string HeartBeat = "heart-beat";
        public const string Id = "id";
        public const string Ack = "ack";
        public const string CorrelationId = "correlation-id";
        public const string ReplyTo = "reply-to";
        public const string UserId = "user-id";
        public const string Login = "login";
        public const string Passcode = "passcode";
        public const string AppId = "app-id";
    }
}