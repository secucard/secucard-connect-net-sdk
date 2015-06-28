namespace secucard.connect
{
    using Secucard.Connect.Rest;
    using Secucard.Model;

    public class RestService : RestBase
    {

        public RestService(RestConfig restconfig)
            : base(new RestConfig { BaseUrl = restconfig.BaseUrl })
        {
        }

        public T Get<T>(RestRequest request) where T : class
        {
            var ret  = RestGet(request);

            return JsonSerializer.DeserializeJson<T>(ret); ;
        }

    }
}