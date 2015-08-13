namespace Secucard.Connect.Product.Smart
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Smart.Event;
    using Transaction = Secucard.Connect.Product.Smart.Model.Transaction;

    public class SmartTransactionsService : ProductService<Transaction>
    {
        public SmartTransactionCashierEventHandler SmartTransactionCashierEvent;

        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, "general.notifications", "display", OnNewEvent);
        }

        protected override ServiceMetaData<Transaction> CreateMetaData()
        {
            return new ServiceMetaData<Transaction>("smart", "transactions");
        }

        private void OnNewEvent(object obj)
        {
            if (SmartTransactionCashierEvent != null)
                SmartTransactionCashierEvent(this,
                    new SmartTransactionCashierEventArgs {SecucardEvent = (Event<Notification>) obj});
        }

        /// <summary>
        ///     Start created transaction with the given transactionId.
        /// </summary>
        public Transaction Start(string transactionId, string type)
        {
            return Execute<Transaction>(transactionId, "start", type, null,
                new ChannelOptions {Channel = ChannelOptions.CHANNEL_STOMP});
        }

        /// <summary>
        ///     Cancel the existing transaction with the given transactionId.
        /// </summary>
        public bool Cancel(string transactionId)
        {
            return ExecuteToBool(transactionId, "cancel", null, null, null);
        }
    }
}