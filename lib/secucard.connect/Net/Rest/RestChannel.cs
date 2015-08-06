namespace Secucard.Connect.Net.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.rest;

    public class RestChannel : Channel
    {
        private RestConfig RestConfig;
        private string ChannelId;
        private RestService RestService;

        public RestChannel(RestConfig restConfig, ClientContext clientContext)
            : base(clientContext)
        {
            RestConfig = restConfig;

            RestService = new RestService(RestConfig);
        }


        #region ## Channel ###

        public override T Request<T>(ChannelRequest channelRequest)
        {
            switch (channelRequest.Method)
            {
                case ChannelMethod.GET:
                    return GetObject<T>(channelRequest.ObjectId);
                case ChannelMethod.CREATE:
                    return CreateObject<T>((T)channelRequest.Object);
                case ChannelMethod.UPDATE:
                    return UpdateObject((T)channelRequest.Object);
                case ChannelMethod.EXECUTE:
                    return Execute<T>(channelRequest.ObjectId, channelRequest.Action, channelRequest.ActionArgs, channelRequest.Object);
                case ChannelMethod.DELETE:
                    DeleteObject<T>(channelRequest.ObjectId);
                    break;
            }
            return null;
        }

        public override ObjectList<T> RequestList<T>(ChannelRequest channelRequest)
        {
            switch (channelRequest.Method)
            {
                case ChannelMethod.GET:
                    return FindObjects<T>(channelRequest.QueryParams);
            }
            return null;
        }

        public override void Open()
        {
            // No socket or http connection init in .NET
        }

        public override void Close()
        {
        }

        private T GetObject<T>(string id) where T:SecuObject
        {
            var request = CreateRequest<T>();
            request.Id = id;
            var obj = RestService.GetObject<T>(request);
            return obj;
        }

        public ObjectList<T> FindObjects<T>(QueryParams query) where T : SecuObject
        {
            var request = CreateRequest<T>();
            request.QueyParams = query;
            var list = RestService.GetList<T>(request);
            return list;
        }

        private T CreateObject<T>(T obj) where T : SecuObject
        {
            var request = CreateRequest<T>();
            request.Object = obj;
            var newObj = RestService.PostObject<T>(request);
            return newObj;
        }

        public T UpdateObject<T>(T obj) where T : SecuObject
        {
            var request = CreateRequest<T>();
            request.Object = obj;
            var newObj = RestService.PutObject<T>(request);
            return newObj;
        }

        public void DeleteObject<T>(string objectId) where T : SecuObject
        {
            var request = CreateRequest<T>();
            request.Id = objectId;
            RestService.DeleteObject<T>(request);
        }

        public T Execute<T>(string id, string action, List<string> actionParameter, object arg) where T : SecuObject
        {
            var request = CreateRequest<T>();
            request.Id = id;
            request.Action = action;
            request.ActionParameter = actionParameter;
            request.Object = arg;
            var newObj = RestService.Execute<T>(request);
            return newObj;
        }

        private RestRequest CreateRequest<T>()
        {
            var obj = (T)Activator.CreateInstance(typeof(T)) as SecuObject;

            // Path resolver for REST
            var resourceString = string.Join("/", obj.ServiceResourceName.Split('.').ToList().Select(s => s.FirstCharToUpper()));

            var token = Context.TokenManager.GetToken(true);
            var request = new RestRequest
            {
                Token = token,
                PageUrl = resourceString,
                Host = "core-dev10.secupay-ag.de" // TODO: Config
            };
            return request;
        }

        #endregion

    }
}
