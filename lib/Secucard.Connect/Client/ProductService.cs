namespace Secucard.Connect.Client
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.Common.Model;

    public abstract class ProductService<T> : IService where T : SecuObject
    {
        protected abstract ServiceMetaData<T> GetMetaData();

        public ClientContext Context { get; set; }

        public virtual void RegisterEvents()
        {
        }

        protected virtual ChannelOptions GetDefaultOptions()
        {
            return ChannelOptions.GetDefault();
        }

        #region ### Get, GetList ###

        public T Get(string id)
        {
            return Get(id, GetDefaultOptions());
        }

        private T Get(string id, ChannelOptions options)
        {
            return Request<T>(new ChannelRequest
            {
                Method = ChannelMethod.Get,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                ObjectId = id,
                ChannelOptions = options
            }, options);
        }

        public ObjectList<T> GetList(QueryParams queryParams)
        {
            return GetList(queryParams, GetDefaultOptions());
        }

        private ObjectList<T> GetList(QueryParams queryParams, ChannelOptions options)
        {
            return RequestList<T>(new ChannelRequest
            {
                Method = ChannelMethod.Get,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                QueryParams = queryParams,
                ChannelOptions = options
            }, options);
        }

        #endregion

        #region ### Create ###

        public T Create(T obj)
        {
            return Create(obj, null);
        }

        private T Create(T obj, ChannelOptions options)
        {
            return Request<T>(new ChannelRequest
            {
                Method = ChannelMethod.Create,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                Object = obj
            }, options);
        }

        #endregion

        #region ### Update ###

        public R Update<R>(R obj) where R : SecuObject
        {
            return Update(obj, null);
        }

        private R Update<R>(R obj, ChannelOptions options) where R : SecuObject
        {
            return Request<R>(new ChannelRequest
            {
                Method = ChannelMethod.Update,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                ObjectId = obj.Id,
                Object = obj
            }, options);
        }

        protected R Update<R>(string id, string action, string actionArg, object obj, ChannelOptions options)
            where R : SecuObject
        {
            var actionArgs = new List<string>();
            if (actionArg != null) actionArgs.Add(actionArg);
            return Request<R>(new ChannelRequest
            {
                Method = ChannelMethod.UpdateWithArgs,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                Action = action,
                ActionArgs = actionArgs,
                ObjectId = id,
                Object = obj
            }, options);
        }

        protected bool UpdateToBool(string id, string action, string actionArg, object obj, ChannelOptions options)
        {
            var actionArgs = new List<string>();
            if (actionArg != null) actionArgs.Add(actionArg);
            var result = Request<ExecuteResult>(new ChannelRequest
            {
                Method = ChannelMethod.UpdateWithArgs,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                Action = action,
                ActionArgs = actionArgs,
                ObjectId = id,
                Object = obj
            }, options);

            return Convert.ToBoolean(result.Result);
        }

        #endregion

        #region ### Delete ###

        public void Delete<R>(string id) where R : SecuObject
        {
            Delete<R>(id, null);
        }

        private void Delete<R>(string id, ChannelOptions options) where R : SecuObject
        {
            Request<R>(new ChannelRequest
            {
                Method = ChannelMethod.Delete,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                ObjectId = id
            }, options);
        }

        #endregion

        #region ### Exectute ###

        protected R Execute<R>(string id, string action, string actionArg, object obj, ChannelOptions options)
            where R : SecuObject
        {
            var actionArgs = new List<string>();
            if (actionArg != null) actionArgs.Add(actionArg);
            return Request<R>(new ChannelRequest
            {
                Method = ChannelMethod.Execute,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                Action = action,
                ActionArgs = actionArgs,
                ObjectId = id,
                Object = obj
            }, options);
        }

        protected bool ExecuteToBool(string id, string action, string actionArg, object obj, ChannelOptions options)
        {
            var actionArgs = new List<string>();
            if (actionArg != null) actionArgs.Add(actionArg);

            var result = Request<ExecuteResult>(new ChannelRequest
            {
                Method = ChannelMethod.Execute,
                Product = GetMetaData().Product,
                Resource = GetMetaData().Resource,
                Action = action,
                ActionArgs = actionArgs,
                ObjectId = id,
                Object = obj
            }, options);

            return Convert.ToBoolean(result.Result);
        }

        #endregion

        #region ### Run Request ###

        private R Request<R>(ChannelRequest channelRequest, ChannelOptions options)
        {
            if (options == null)
            {
                options = GetDefaultOptions();
            }
            if (channelRequest.ChannelOptions == null)
            {
                channelRequest.ChannelOptions = options;
            }
            var channel = GetChannelByOptions(options);

            var result = channel.Request<R>(channelRequest);
            return result;
        }

        private ObjectList<R> RequestList<R>(ChannelRequest channelRequest, ChannelOptions options) where R : SecuObject
        {
            if (options == null)
            {
                options = GetDefaultOptions();
            }
            //if (p.options == null) {
            //    p.options = options;
            //}
            //final Callback.Notify processor = options.resultProcessing;
            var channel = GetChannelByOptions(options);
            var result = channel.RequestList<R>(channelRequest);
            //if (processor != null) {
            //    processor.notify(result);
            //}
            return result;
        }

        #endregion

        /// <summary>
        /// Select and return an API communication channel according to the provided options.
        /// </summary>
        private Channel GetChannelByOptions(ChannelOptions options)
        {
            // select channel according to special product preferences or fall back to default
            if (options.Channel == null)
            {
                options.Channel = Context.DefaultChannel;
            }
            var channel = Context.Channels[options.Channel];

            if (channel == null)
            {
                throw new Exception("Can't use " + GetMetaData().Product + " with " + options.Channel);
            }
            return channel;
        }
    }
}