namespace Secucard.Connect.Product
{
    using System.Collections.Generic;
    using Secucard.Connect.Channel;
    using Secucard.Model;

    public abstract class AbstractService
    {
        public ClientContext Context;

        protected IChannel GetChannel()
        {
            // TODO: Make a choice
            return Context.RestChannel;
        }

        protected T Create<T>(T obj) where T : SecuObject
        {
            return GetChannel().CreateObject(obj);
        }

        protected T Update<T>(T obj) where T : SecuObject
        {
            return GetChannel().UpdateObject(obj);
        }

        protected void Delete<T>(string objectId) where T : SecuObject
        {
            GetChannel().DeleteObject<T>(objectId);
        }

        protected ObjectList<T> GetList<T>(QueryParams queryParams) where T : SecuObject
        {
            return GetChannel().FindObjects<T>(queryParams);
        }

        protected T Execute<T, U>(string appId, string action, List<string> actionParameter, U obj) where T : SecuObject
        {
            return GetChannel().Execute<T, U>(appId, action, actionParameter, obj);
        }

    }
}
