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

namespace Secucard.Connect.Client
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.Channel;
    using Secucard.Connect.Net;
    using Secucard.Model;

    public interface IService
    {
        ClientContext Context { get; set; }

    }

    public abstract class ProductService<T>:IService where T : SecuObject
    {
        private ServiceMetaData<T> MetaData;
        public ClientContext Context {  get;set; }
        private T ProductType;

        public ProductService()
        {
            MetaData = CreateMetaData();
        }


        //public void SetContext(ClientContext context)
        //{
        //    Context = context;
        //}

        /**
   * Creates meta data associated with this product service.
   * Don't use for retrieval, use {@link #getMetaData()} instead.
   */
        protected abstract ServiceMetaData<T> CreateMetaData();


        protected ChannelOptions GetDefaultOptions()
        {
            return ChannelOptions.GetDefault();
        }

        //public ServiceMetaData<T> getMetaData()
        //{
        //    if (MetaData == null)
        //    {
        //        MetaData = CreateMetaData();
        //    }
        //    return MetaData;
        //}

        // get, get list -----------------------------------------------------------------------------------------------------

        public T Get(string id)
        {
            return Get(id, GetDefaultOptions());
        }

        private T Get(string id, ChannelOptions options)
        {
            return Request<T>(new ChannelRequest
            {
                Method = ChannelMethod.GET,
                Product = MetaData.Product,
                Resource = MetaData.Resource,
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
                Method = ChannelMethod.GET,
                Product = MetaData.Product,
                Resource = MetaData.Resource,
                QueryParams = queryParams,
                ChannelOptions = options
            }, options);
        }

        //     /**
        //* Like {@link #getSimpleList(com.secucard.connect.product.common.model.QueryParams, com.secucard.connect.client.Callback)}
        //* but without callback.
        //*/
        //     public List<T> getSimpleList(QueryParams queryParams) {
        //         return getSimpleList(queryParams, null);
        //     }

        /**
   * Get simple list of product resources.
   *
   * @param queryParams Contains the query params to apply.
   * @param callback    Callback for getting the results asynchronous.
   * @return The resource objects. Null if nothing found.
   */
        //public List<T> getSimpleList(QueryParams queryParams) {
        //    return getSimpleList(queryParams, null, callback);
        //}

        //protected List<T> getSimpleList(QueryParams queryParams, ApiOptions options) {
        //    Converter.ToListConverter<T> converter = new Converter.ToListConverter<>();
        //    CallbackAdapter<ObjectList<T>, List<T>> cb = callback == null ? null : new CallbackAdapter<>(callback, converter);
        //    ObjectList<T> list = requestList(Channel.Method.GET,
        //        new Channel.Params(getObject(), queryParams, getResourceType(), options), options, cb);
        //    return converter.convert(list);
        //}


        // create ------------------------------------------------------------------------------------------------------------

        /**
   * Like {@link #create(com.secucard.connect.product.common.model.SecuObject, com.secucard.connect.client.Callback)}
   * but without callback.
   */

        /**
   * Creates a new product resource and returns the result. The server may add additional default data to it
   * so additional field may be filled in the result, like id. Use this result for further processing instead of the
   * provided.
   * <p/>
   * If the resource can't be created or another error happens ClientError will be thrown.
   * Inspect the code and userMessage field to get info about the error cause.
   *
   * @param object   The resource to create.
   * @param callback Callback receiving the result asynchronous.
   */
        public T Create(T obj)
        {
            return Create(obj, null);
        }

        protected T Create(T obj, ChannelOptions options)
        {
            return Request<T>(new ChannelRequest
                   {
                       Method = ChannelMethod.CREATE,
                       Product = MetaData.Product,
                       Resource = MetaData.Resource,
                       Object = obj
                   }, options);
        }


        // update ------------------------------------------------------------------------------------------------------------

        /**
   * Like {@link #update(com.secucard.connect.product.common.model.SecuObject, com.secucard.connect.client.Callback)}
   * but without callback.
   */

        /**
   * Updates a product resource and returns the result. The server may add additional default data to it
   * so additional field may be filled in the result. Use this result for further processing instead of the
   * provided.
   * <p/>
   * If the resource can't be updated or another error happens ClientError will be thrown.
   * Inspect the code and userMessage field to get info about the error cause.
   *
   * @param object   The resource to update.
   * @param callback Callback receiving the result asynchronous.
   */
        public T Update(T obj) {
            return Update<T>(obj, null);
        }

        protected T Update<T>(T obj, ChannelOptions options) where T : SecuObject
        {
            return Request<T>(new ChannelRequest
               {
                   Method = ChannelMethod.UPDATE,
                   Product = MetaData.Product,
                   Resource = MetaData.Resource,
                   ObjectId = obj.Id,
                   Object = obj
               }, options);
        }

        //protected <R> R Update(string id, string action, string actionArg, object object, Class<R> returnType,
        //    ChannelOptions options) {
        //        return request(Connect.Channel.Method.UPDATE,
        //            new Connect.Channel.Params(getObject(), id, action, actionArg, object, returnType, options), options);
        //    }

        //protected bool updateToBool(string id, string action, string actionArg, object object, Options options,
        //    Callback<bool> callback) {
        //        Converter<Result, bool> converter = Converter.RESULT2BOOL;
        //        CallbackAdapter<Result, bool> cb = callback == null ? null : new CallbackAdapter<>(callback, converter);
        //        Result result = request(Channel.Method.UPDATE,
        //            new Channel.Params(getObject(), id, action, actionArg, object, Result.class, options), options, cb);
        //        return converter.convert(result);
        //    }

        // delete ------------------------------------------------------------------------------------------------------------

        // TODO: Remove Type from Signatur
        public void Delete<T>(string id) where T : SecuObject
        {
            Delete<T>(id, null);
        }

        protected void Delete<T>(string id, ChannelOptions options) where T : SecuObject
        {
            Request<T>(new ChannelRequest
            {
                Method = ChannelMethod.DELETE,
                Product = MetaData.Product,
                Resource = MetaData.Resource,
                ObjectId = id
            }, options);

        }

        //protected void delete(string id, string action, string actionArg, Options options, Callback<void> callback) {
        //    request(Connect.Channel.Method.DELETE, new Connect.Channel.Params(getObject(), id, action, actionArg, options), options, callback);
        //}

        // execute -----------------------------------------------------------------------------------------------------------

        protected R Execute<R>(string id, string action, string actionArg, object obj, ChannelOptions options) where R : SecuObject
        {
            return Request<R>(new ChannelRequest
                 {
                     Method = ChannelMethod.DELETE,
                     Product = MetaData.Product,
                     Resource = MetaData.Resource,
                     Action = action,
                     ActionArgs = new List<string>() { actionArg },
                     ObjectId = id,
                     Object = obj
                 }, options);
        }

        //protected <R> R execute(string action, object object, Class<R> returnType, Options options, Callback<R> callback) {
        //    return execute(getAppId(), action, object, returnType, options, callback);
        //}

        //protected <R> R execute(string appId, string action, object object, Class<R> returnType, Options options,
        //Callback<R> callback) {
        //    return request(Connect.Channel.Method.EXECUTE, Connect.Channel.Params.forApp(appId, action, object, returnType, options), options,
        //        callback);
        //}

        //protected bool executeToBool(string id, string action, string actionArg, object object, Options options,
        //    Callback<bool> callback) {
        //        Converter<Result, bool> converter = Converter.RESULT2BOOL;
        //        CallbackAdapter<Result, bool> cb = callback == null ? null : new CallbackAdapter<>(callback, converter);
        //        Result result = request(Connect.Channel.Method.EXECUTE, new Connect.Channel.Params(getObject(), id, action, actionArg, object,
        //            Result.class, options), options, cb);
        //        return converter.convert(result);
        //    }

        //protected bool executeToBool(string action, object object, Options options, Callback<bool> callback) {
        //    return executeToBool(getAppId(), action, object, options, callback);
        //}

        //protected bool executeToBool(string appId, string action, object object, Options options, Callback<bool> callback) {
        //    Converter<Result, bool> converter = Converter.RESULT2BOOL;
        //    CallbackAdapter<Result, bool> cb = callback == null ? null : new CallbackAdapter<>(callback, converter);
        //    Result result = request(Connect.Channel.Method.EXECUTE, Connect.Channel.Params.forApp(appId, action, object, Result.class, options),
        //    options, cb);
        //    return converter.convert(result);
        //}

        // ---------------------------------------------------------------------------------------------------

        private R Request<R>(ChannelRequest channelRequest, ChannelOptions options) where R : SecuObject
        {
            if (options == null)
            {
                options = GetDefaultOptions();
            }
            if (channelRequest.ChannelOptions == null)
            {
                channelRequest.ChannelOptions = options;
            }
            //final Callback.Notify pre = options.resultProcessing;
            var channel = GetChannelByOptions(options);
            try
            {
                R result = channel.Request<R>(channelRequest);
                //if (pre != null) {
                //    pre.notify(result);
                //}
                return result;
            }
            catch (Exception e)
            {
                throw e;
                //handleException(e);
            }

            //        else {
            //            channel.request(method, p, new Callback<R>() {
            //            public void completed(R result) {
            //if (pre != null) {
            //pre.notify(result);
            //            }
            //            callback.completed(result);
            //        }

            //public void failed(Throwable cause) {
            //    Exception ex = ExceptionMapper.map(cause, null);
            //    if (context.exceptionHandler == null) {
            //        callback.failed(ex);
            //    } else {
            //        context.exceptionHandler.handle(ex);
            //    }
            //}
            //});
            //}
            //return null;
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
            try
            {
                ObjectList<R> result = channel.RequestList<R>(channelRequest);
                //if (processor != null) {
                //    processor.notify(result);
                //}
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /**
   * Translate and pass given throwable to the exception handler if any, else just throw.
   */
        //private void handleException(Throwable throwable) {
        //    RuntimeException ex = ExceptionMapper.map(throwable, null);
        //    if (context.exceptionHandler == null) {
        //        throw ex;
        //    }
        //    context.exceptionHandler.handle(ex);
        //}

        /**
   * Select and return an API communication channel according to the provided options.
   */
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
                throw new Exception("Can't use " + MetaData.Product + " with " + options.Channel);
            }
            return channel;
        }
    }
}
