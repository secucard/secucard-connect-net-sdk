namespace Secucard.Connect.Net.Rest
{
    using System;

    public class RestException : Exception
    {
        public string BodyText { get; set; }
        public int? StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}