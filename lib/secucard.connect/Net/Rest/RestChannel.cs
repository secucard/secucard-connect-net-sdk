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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;

    public class RestChannel : Channel
    {
        private readonly RestConfig RestConfig;
        private string ChannelId;
        private readonly RestService RestService;

        public RestChannel(RestConfig restConfig, ClientContext clientContext)
            : base(clientContext)
        {
            RestConfig = restConfig;

            RestService = new RestService(RestConfig.BaseUrl);
        }


        #region ## Channel ###

        public override T Request<T>(ChannelRequest channelRequest)
        {
            var request = CreateRequest(channelRequest);

            switch (channelRequest.Method)
            {
                case ChannelMethod.GET:
                    return GetObject<T>(request, channelRequest.ObjectId);
                case ChannelMethod.CREATE:
                    return CreateObject<T>(request, (T)channelRequest.Object);
                case ChannelMethod.UPDATE:
                    return UpdateObject(request, channelRequest.ObjectId, (T)channelRequest.Object);
                case ChannelMethod.EXECUTE:
                    return Execute<T>(request, channelRequest.ObjectId, channelRequest.Action, channelRequest.ActionArgs, channelRequest.Object);
                case ChannelMethod.DELETE:
                    DeleteObject<T>(request, channelRequest.ObjectId);
                    break;
            }
            return default(T);
        }

        public override ObjectList<T> RequestList<T>(ChannelRequest channelRequest)
        {
            var request = CreateRequest(channelRequest);

            switch (channelRequest.Method)
            {
                case ChannelMethod.GET:
                    return FindObjects<T>(request, channelRequest.QueryParams);
            }
            return null;
        }

        public override void Open()
        {
            // No socket or http connection init in .NET
        }

        public override void Close()
        {
            // No socket or http connection to close in .NET

        }

        private T GetObject<T>(RestRequest request, string id) 
        {
            request.Id = id;
            var obj = RestService.GetObject<T>(request);
            return obj;
        }

        private ObjectList<T> FindObjects<T>(RestRequest request, QueryParams query) 
        {
            request.QueryParams = query;
            var list = RestService.GetList<T>(request);
            return list;
        }

        private T CreateObject<T>(RestRequest request, T obj)
        {
            request.Object = obj;
            var newObj = RestService.PostObject<T>(request);
            return newObj;
        }

        private T UpdateObject<T>(RestRequest request, string id, T obj)
        {
            request.Object = obj;
            request.Id = id;
            var newObj = RestService.PutObject<T>(request);
            return newObj;
        }

        private void DeleteObject<T>(RestRequest request, string objectId) 
        {
            request.Id = objectId;
            RestService.DeleteObject<T>(request);
        }

        private T Execute<T>(RestRequest request, string id, string action, List<string> actionParameter, object arg) 
        {
            request.Id = id;
            request.Action = action;
            request.ActionParameter = actionParameter;
            request.Object = arg;
            var newObj = RestService.Execute<T>(request);
            return newObj;
        }

        private RestRequest CreateRequest(ChannelRequest channelRequest)
        {
            var token = Context.TokenManager.GetToken(true);
            var request = new RestRequest
            {
                Token = token,
                PageUrl = channelRequest.Product.FirstCharToUpper() + "/" + channelRequest.Resource.FirstCharToUpper(),
                Host = new Uri(RestConfig.BaseUrl).Host 
            };
            return request;
        }

        #endregion

    }
}
