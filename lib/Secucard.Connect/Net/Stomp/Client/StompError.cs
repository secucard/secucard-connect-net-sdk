namespace Secucard.Connect.Net.Stomp.Client
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Exception wrapping a STOMP error frame.
    /// </summary>
    public class StompError : Exception
    {
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public StompError(string body, Dictionary<string, string> headers)
        {
            Body = body;
            Headers = headers;
        }
    }
}