namespace Secucard.Connect.Product.Common.Model
{
    using System;
    using System.Globalization;
    using System.Linq;

    public static class Linq
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