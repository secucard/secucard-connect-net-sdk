namespace Secucard.Model
{
    using System;
    using System.Globalization;

    public static class Linq
    {
        public static DateTime? ToDateTime(this string s)
        {
            var d = DateTime.ParseExact(s, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal );
            return d;
        }

        public static string ToDateTimeZone(this DateTime? d)
        {
            return d.HasValue ? d.Value.ToString("yyy-MM-ddTHH:mm:sszzz") : null;
        }
    }
}
