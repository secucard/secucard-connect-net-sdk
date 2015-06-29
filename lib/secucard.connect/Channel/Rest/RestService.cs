namespace secucard.connect
{
    using System.Collections.Generic;
    using Secucard.Connect.Rest;
    using Secucard.Model;

    public class RestService : RestBase
    {

        public RestService(RestConfig restconfig)
            : base(new RestConfig { BaseUrl = restconfig.BaseUrl })
        {
        }

        public ObjectList<T> GetList<T>(RestRequest request) where T : SecuObject
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<ObjectList<T>>(ret); ;
        }

        public T GetObject<T>(RestRequest request) where T : SecuObject
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<T>(ret); ;
        }

    }
}