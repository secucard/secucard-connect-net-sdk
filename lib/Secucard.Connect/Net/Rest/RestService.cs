/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Net.Rest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    public class RestService : RestBase
    {
        public RestService(RestConfig restConfig)
            : base(restConfig)
        {
        }

        public ObjectList<T> GetList<T>(RestRequest request)
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<ObjectList<T>>(ret);
        }

        public T GetObject<T>(RestRequest request)
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public T PostObject<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJson((T) request.Object);
            var ret = RestPost(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public List<T> PostObjectList<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJsonList(request.Objects.Cast<T>().ToList());
            var ret = RestPost(request);

            return JsonSerializer.DeserializeJsonList<T>(ret);
        }

        public T PutObject<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJson((T) request.Object);
            var ret = RestPut(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public void DeleteObject(RestRequest request)
        {
            RestDelete(request);
        }

        public T Execute<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJson(request.Object);
            var ret = RestExecute(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public Stream GetStream(RestRequest request)
        {
            return RestGetStream(request);
        }
    }
}