namespace Secucard.Connect.Channel.Rest
{
    using System.Collections.Generic;
    using System.Linq;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.Smart;

    public class RestService : RestBase
    {

        public RestService(RestConfig restconfig)
            : base(new RestConfig { BaseUrl = restconfig.BaseUrl })
        {
        }

        public ObjectList<T> GetList<T>(RestRequest request)
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<ObjectList<T>>(ret); ;
        }

        public T GetObject<T>(RestRequest request) where T : SecuObject
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<T>(ret); ;
        }

        public T PostObject<T>(RestRequest request) where T : SecuObject
        {
            request.BodyJsonString  = JsonSerializer.SerializeJson((T)request.Object);
            var ret = RestPost(request);

            return JsonSerializer.DeserializeJson<T>(ret); ;
        }

        public List<T> PostObjectList<T>(RestRequest request) where T : SecuObject
        {
            request.BodyJsonString = JsonSerializer.SerializeJsonList<T>(request.Objects.Cast<T>().ToList());
            var ret = RestPost(request);

            return JsonSerializer.DeserializeJsonList<T>(ret); ;
        }

        public T PutObject<T>(RestRequest request) where T : SecuObject
        {
            request.Id = request.Object.Id;
            request.BodyJsonString = JsonSerializer.SerializeJson((T)request.Object);
            var ret = RestPut(request);

            return JsonSerializer.DeserializeJson<T>(ret); ;
        }

        public T DeleteObject<T>(RestRequest request) where T : SecuObject
        {
            var ret = RestDelete(request);

            return JsonSerializer.DeserializeJson<T>(ret); ;
        }
    }
}