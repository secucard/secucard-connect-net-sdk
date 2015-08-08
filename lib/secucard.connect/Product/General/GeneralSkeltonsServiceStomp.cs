namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.General.Model;

    public class GeneralSkeltonsServiceStomp : ProductService<Skeleton>
    {
        protected override ServiceMetaData<Skeleton> CreateMetaData()
        {
            return new ServiceMetaData<Skeleton>("general", "skeletons");
        }

        protected override ChannelOptions GetDefaultOptions()
        {
           var channelOptions =  base.GetDefaultOptions();
            channelOptions.Channel = ChannelOptions.CHANNEL_STOMP;
            return channelOptions;
        }

        //public Skeleton GetSkeleton(string id)
        //{
        //    return GetChannel().GetObject<Skeleton>(id);
        //}

        //public ObjectList<Skeleton> GetSkeletons(QueryParams queryParams)
        //{
        //    return GetChannel().FindObjects<Skeleton>(queryParams);
        //}
    }
}
