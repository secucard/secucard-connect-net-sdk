namespace Secucard.Connect.Channel.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Model;

    public class RestChannel : AbstractChannel, IChannel
    {
        private RestConfig RestConfig;
        private string ChannelId;
        private RestService RestService;
        private AuthProvider AuthProvider;

        public RestChannel(RestConfig restConfig, string channelId, AuthProvider authProvider)
        {
            RestConfig = restConfig;
            ChannelId = channelId;// TODO: Needed?
            AuthProvider = authProvider;

            RestService = new RestService(RestConfig);
        }


        #region ## IChannel ###

        public void Open()
        {
            // No socket or http connection init in .NET
        }

        public T GetObject<T>(string id) where T : SecuObject
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

        public T CreateObject<T>(T obj) where T : SecuObject
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

        public void DeleteObject<T>(string objectId) where T:SecuObject
        {
            var request = CreateRequest<T>();
            request.Id = objectId;
            RestService.DeleteObject<T>(request);
        }

        public T Execute<T, U>(string id, string action, List<string> actionParameter,  object arg) where T : SecuObject
        {
            var request = CreateRequest<T>();
            request.Id = id;
            request.Action = action;
            request.ActionParameter = actionParameter;
            request.Object = arg;
            var newObj = RestService.Execute<T, U>(request);
            return newObj;
        }

        private RestRequest CreateRequest<T>()
        {
            var obj = (T)Activator.CreateInstance(typeof(T)) as SecuObject;

            // Path resolver for REST
            var resourceString = string.Join("/", obj.ServiceResourceName.Split('.').ToList().Select(s => s.FirstCharToUpper()));

            var token = AuthProvider.GetToken(true);
            var request = new RestRequest
            {
                Token = token.AccessToken,
                PageUrl = resourceString,
                Host = "core-dev10.secupay-ag.de" // TODO: Config
            };
            return request;
        }

        #endregion

    }
}
