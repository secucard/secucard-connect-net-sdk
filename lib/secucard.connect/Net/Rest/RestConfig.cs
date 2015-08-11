namespace Secucard.Connect.Net.Rest
{
    using Secucard.Connect.Client.Config;

    public class RestConfig
    {
        public string BaseUrl { get; set; }

        public RestConfig(Properties properties)
        {
            BaseUrl = properties.Get("Rest.BaseUrl");
        }
    }
}
