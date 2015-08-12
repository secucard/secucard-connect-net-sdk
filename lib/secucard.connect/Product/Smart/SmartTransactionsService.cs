namespace Secucard.Connect.Product.Smart
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.Smart.Model;

    public class SmartTransactionsService : ProductService<Transaction>
    {
        protected override ServiceMetaData<Transaction> CreateMetaData()
        {
            return new ServiceMetaData<Transaction>("smart", "transactions");
        }

        public Transaction Start(string transactionId, string type)
        {
            // TODO: Call on STOMP
            return Execute<Transaction>(transactionId, "start", type, null, new ChannelOptions { Channel = ChannelOptions.CHANNEL_STOMP });
        }

        ///**
        // * Set a callback to get notified when a cashier notification arrives.
        // */
        //public void onCashierDisplayChanged(Callback<Notification> callback) {

        //    AbstractEventListener listener = null;

        //  if (callback != null) {
        //    listener = new DelegatingEventHandlerCallback<Notification, Notification>(callback) {
        //      public boolean accept(Event event) {
        //        return Events.TYPE_DISPLAY.equals(event.getType()) && "general.notifications".equals(event.getTarget());
        //      }

        //      protected Notification process(Event<Notification> event) {
        //        return event.getData();
        //      }
        //    };
        //  }

        //  context.eventDispatcher.registerListener(Events.TYPE_DISPLAY + "general.notifications", listener);
        //}

        /// <summary>
        /// Cancel the existing transaction with the given transactionId.
        /// </summary>
        public bool Cancel(string transactionId)
        {
            return ExecuteToBool(transactionId, "cancel", null, null, null);
        }

    }
}
