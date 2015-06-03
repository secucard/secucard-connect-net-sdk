namespace secucard.connect.Rest
{
    using System;

    public class RestException : Exception
    {
        public string BodyText { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}