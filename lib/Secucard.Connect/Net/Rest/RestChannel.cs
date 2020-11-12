namespace Secucard.Connect.Net.Rest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    public class RestChannel : Channel
    {
        private readonly RestConfig _restConfig;
        private readonly RestService _restService;

        public RestChannel(RestConfig restConfig, ClientContext clientContext)
            : base(clientContext)
        {
            _restConfig = restConfig;

            _restService = new RestService(_restConfig);
        }

        #region ## Channel ###

        public override T Request<T>(ChannelRequest channelRequest)
        {
            var request = CreateRequest(channelRequest);

            try
            {
                switch (channelRequest.Method)
                {
                    case ChannelMethod.Get:
                        return GetObject<T>(request, channelRequest.ObjectId, channelRequest.Action, channelRequest.ActionArgs,
                            channelRequest.Object);
                    case ChannelMethod.Create:
                        return CreateObject(request, (T)channelRequest.Object);
                    case ChannelMethod.Update:
                        return UpdateObject(request, channelRequest.ObjectId, (T)channelRequest.Object);
                    case ChannelMethod.UpdateWithArgs:
                        return UpdateObject<T>(request, channelRequest.ObjectId, channelRequest.Action, channelRequest.ActionArgs,
                            channelRequest.Object);
                    case ChannelMethod.Execute:
                        return Execute<T>(request, channelRequest.ObjectId, channelRequest.Action, channelRequest.ActionArgs,
                            channelRequest.Object);
                    case ChannelMethod.Delete:
                        DeleteObject<T>(request, channelRequest.ObjectId);
                        break;
                }
            }
            catch (RestException ex)
            {
                var status = JsonSerializer.TryDeserializeJson<Status>(ex.BodyText);
                if (status != null)
                {
                    throw new ProductException(status.Error + " --> " + status.ErrorDetails + " (SupportId: " + status.SupportId + ")")
                    {
                        Status = status
                    };
                }
                throw;
            }

            return default(T);
        }

        public override ObjectList<T> RequestList<T>(ChannelRequest channelRequest)
        {
            var request = CreateRequest(channelRequest);

            switch (channelRequest.Method)
            {
                case ChannelMethod.Get:
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

        private T GetObject<T>(RestRequest request, string id, string action = null, List<string> actionParameter = null, object obj = null)
        {
            request.Object = obj;
            request.Id = id;
            request.Action = action;
            request.ActionParameter = actionParameter;
            var newObj = _restService.GetObject<T>(request);
            return newObj;
        }

        private ObjectList<T> FindObjects<T>(RestRequest request, QueryParams query)
        {
            request.QueryParams = query;
            var list = _restService.GetList<T>(request);
            return list;
        }

        private T CreateObject<T>(RestRequest request, T obj)
        {
            request.Object = obj;
            var newObj = _restService.PostObject<T>(request);
            return newObj;
        }

        private T UpdateObject<T>(RestRequest request, string id, T obj)
        {
            request.Object = obj;
            request.Id = id;
            var newObj = _restService.PutObject<T>(request);
            return newObj;
        }

        private T UpdateObject<T>(RestRequest request, string id, string action, List<string> actionParameter, object obj)
        {
            request.Object = obj;
            request.Id = id;
            request.Action = action;
            request.ActionParameter = actionParameter;
            var newObj = _restService.Put<T>(request);
            return newObj;
        }

        private void DeleteObject<T>(RestRequest request, string objectId)
        {
            request.Id = objectId;
            _restService.DeleteObject(request);
        }

        private T Execute<T>(RestRequest request, string id, string action, List<string> actionParameter, object arg)
        {
            request.Id = id;
            request.Action = action;
            request.ActionParameter = actionParameter;
            request.Object = arg;
            var newObj = _restService.Execute<T>(request);
            return newObj;
        }

        private RestRequest CreateRequest(ChannelRequest channelRequest)
        {
            var token = Context.TokenManager.GetToken(true);
            var request = new RestRequest
            {
                Token = token,
                PageUrl = channelRequest.Product.FirstCharToUpper() + "/" + channelRequest.Resource.FirstCharToUpper(),
                Host = new Uri(_restConfig.Url).Host
            };
            return request;
        }

        #endregion

        public Stream GetStream(RestRequest request)
        {
            var stream = _restService.GetStream(request);
            return stream;
        }
    }
}