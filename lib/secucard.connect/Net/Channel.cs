namespace Secucard.Connect.Net
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;

    public abstract class Channel
    {
        // TODO: Events from Channel


        protected ClientContext Context;

        protected Channel(ClientContext context)
        {
            Context = context;
        }

        public abstract T Request<T>(ChannelRequest channelRequest);


        /**
         * Retrieving a list of objects of any type.
         *
         * @param method   The method to use.
         * @param params   The call parameters.
         * @param callback Callback for async processing.
         * @param <T>      The actual object type.
         * @return The object list, null if a callback was used.
         */
        public abstract ObjectList<T> RequestList<T>(ChannelRequest channelRequest) where T : SecuObject;


        /**
         * Registers a listener which gets called when a server side or other event happens.
         * Server side events may not be supported by some channels, e.g. REST based channels!
         * Note that exceptions thrown by listeners methods may be swallowed by the calling thread code silently to prevent
         * breaking the event receiving process.
         *
         * @param listener The listener to notify.
         */
        // TODO

        /**
         * Open the channel and its resources.
         */
        public abstract void Open();

        /**
         * Close channel and release resources.
         */
        public abstract void Close();
    }
}
