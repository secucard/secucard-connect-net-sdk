namespace Secucard.Connect.Product.General
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using Secucard.Model;
    using Secucard.Model.General;

    public class GeneralSkeltonsService: ServiceBase
    {
        public Skeleton GetSkeleton(string id)
        {
            return RestChannel.GetObject<Skeleton>(id);
        }

        public IAsyncResult BeginGetSkeleton(string id, AsyncCallback callback, object asyncState)
        {
            return null;
        }

        public Skeleton EndGetSkeleton(IAsyncResult result)
        {
            return null;
        }

        public ObjectList<Skeleton> GetSkeletons(QueryParams queryParams)
        {
            return RestChannel.FindObjects<Skeleton>(queryParams);
        }
    }
}
