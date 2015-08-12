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
    using System;
    using System.Diagnostics;

    internal static class StompTrace
    {
        internal static void ClientTrace(Exception e)
        {
            ClientTrace("Exception: {0}", e.Message);
            if (e.InnerException != null) ClientTrace("Inner exception: {0}", e.InnerException.Message);
        }

        internal static void ClientTrace(string fmt, params object[] param)
        {
            Trace.TraceInformation(fmt, param);
        }
    }
}