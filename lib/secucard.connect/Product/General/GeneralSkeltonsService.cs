namespace Secucard.Connect.Product.General
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using Secucard.Model;
    using Secucard.Model.General;

    public class GeneralSkeletonsService: AbstractService
    {
        public Skeleton GetSkeleton(string id)
        {
            return GetChannel().GetObject<Skeleton>(id);
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
            return GetChannel().FindObjects<Skeleton>(queryParams);
        }
    }
}
