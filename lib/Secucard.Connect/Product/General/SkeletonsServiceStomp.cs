namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.General.Model;

    [System.Obsolete("Not used any more", true)]
    public class SkeletonsServiceStomp : ProductService<Skeleton>
    {
        public static readonly ServiceMetaData<Skeleton> MetaData = new ServiceMetaData<Skeleton>(
            "general", "skeletons");

        protected override ServiceMetaData<Skeleton> GetMetaData()
        {
            return MetaData;
        }

        protected override ChannelOptions GetDefaultOptions()
        {
            var channelOptions = base.GetDefaultOptions();
            channelOptions.Channel = ChannelOptions.ChannelStomp;
            return channelOptions;
        }
    }
}