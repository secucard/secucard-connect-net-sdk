namespace Secucard.Connect.Rest
{
    using System.Collections.Generic;

    public class RestRequest
    {
        public RestRequest()
        {
            Header = new Dictionary<string, string>();
            Parameter = new Dictionary<string, string>();
        }

        public string Host { get; set; }
        public string PageUrl { get; set; }
        public Dictionary<string, string> Header { get; set; }
        public Dictionary<string, string> Parameter { get; set; }
        public string BodyJsonString { get; set; }
    }
}