namespace Secucard.Connect.Net.Rest
{
    using Secucard.Connect.Client.Config;

    public class RestConfig
    {
        public RestConfig(Properties properties)
        {
            Url = properties.Get("Rest.Url");
            ResponseTimeoutSec = properties.Get("Rest.ResponseTimeoutSec", 300);
            ConnectTimeoutSec = properties.Get("Rest.ConnectTimeoutSec", 300);
        }

        public RestConfig()
        {
            ResponseTimeoutSec = 300;
            ConnectTimeoutSec = 300;
        }

        public string Url { get; set; }
        public int ResponseTimeoutSec { get; set; }
        public int ConnectTimeoutSec { get; set; }

        public override string ToString()
        {
            return "RestConfig [" + "Url = " + Url + "]";
        }
    }
}