namespace Secucard.Connect.Net
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;

    public abstract class Channel
    {
        protected readonly ClientContext Context;

        protected Channel(ClientContext context)
        {
            Context = context;
        }

        public abstract T Request<T>(ChannelRequest channelRequest);

        /// <summary>
        /// Retrieving a list of objects of any type.
        /// </summary>
        public abstract ObjectList<T> RequestList<T>(ChannelRequest channelRequest) where T : SecuObject;

        /// <summary>
        /// Open the channel and its resources.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// Close channel and release resources.
        /// </summary>
        public abstract void Close();
    }
}