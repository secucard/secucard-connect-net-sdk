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

namespace Secucard.Connect.Client
{
    using System;
    using System.Diagnostics;
    using System.Runtime.ExceptionServices;

    public static class SecucardTrace
    {
        internal static void EmptyLine()
        {
            Trace.WriteLine(string.Empty);
        }

        internal static void Info(string fmt, params object[] param)
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = string.Empty;
            if (type != null) name = type.Name;
            var source = string.Format("{0}.{1}", name, method.Name);

            Trace.WriteLine(string.Format("{0} : {1} : {2}", "info".PadRight(8), source, string.Format(fmt, param)));
        }

        internal static void InfoSource(string source, string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format("{0} : {1} : {2}", "info".PadRight(8), source, string.Format(fmt, param)));
        }

        internal static void Error(string source, string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format("{0} : {1} : {2}", "error".PadRight(8), source, string.Format(fmt, param)));
        }


        internal static void Exception(Exception ex)
        {
            Trace.WriteLine(string.Format("{0} : {1} : {2}", "error".PadRight(8), GetSource(), string.Format("{0}\n{1}", ex.Message, ex.StackTrace)));
        }

        private static string GetSource()
        {
            var frame = new StackFrame(2);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = string.Empty;
            if (type != null) name = type.Name;
            var source = string.Format("{0}.{1}", name, method.Name);
            return source;
        }
    }
}