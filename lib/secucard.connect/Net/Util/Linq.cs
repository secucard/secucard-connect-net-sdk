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

namespace Secucard.Connect.Net.Util
{
    using System;
    using System.Globalization;
    using System.Linq;

    public static class LinqExtentions
    {
        public static DateTime? ToDateTime(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            DateTime d;
            const string fmt = "yyyy-MM-ddTHH:mm:sszzz";
            if (DateTime.TryParseExact(s, fmt, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out d))
            {
                return d;
            }
            return null;
        }

        public static string ToDateTimeZone(this DateTime? d)
        {
            return d.HasValue ? d.Value.ToString("yyy-MM-ddTHH:mm:sszzz") : null;
        }

        public static string FirstCharToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }
    }
}