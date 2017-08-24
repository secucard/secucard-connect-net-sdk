namespace Secucard.Connect.Product.Smart
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Smart.Event;
    using Transaction = Secucard.Connect.Product.Smart.Model.Transaction;

    public class TransactionsService : ProductService<Transaction>
    {
        public TransactionCashierEventHandler TransactionCashierEvent;

        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, "general.notifications", "display", OnNewEvent);
        }

        public static string TYPE_DEMO = "demo";
        public static string TYPE_CASH = "cash";
        public static string TYPE_AUTO = "auto";
        public static string TYPE_ZVT = "cashless";
        public static string TYPE_LOYALTY = "loyalty";

        public static readonly ServiceMetaData<Transaction> MetaData = new ServiceMetaData<Transaction>("smart", "transactions");

        protected override ServiceMetaData<Transaction> GetMetaData()
        {
            return MetaData;
        }

        private void OnNewEvent(object obj)
        {
            if (TransactionCashierEvent != null)
                TransactionCashierEvent(this,
                    new TransactionCashierEventArgs { SecucardEvent = (Event<Notification>)obj });
        }

        /// <summary>
        /// Start created transaction with the given transactionId and type
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Transaction Start(string transactionId, string type)
        {
            // Load default properties
            var properties = Properties.Load("SecucardConnect.config");

            /*
              <!-- STOMP server communication is enabled: true | false/nothing -->
              If STOMP server communication is disabled then you have to use REST
            */

            var channel = string.Empty;

            if (properties.Get("Stomp.Enabled", false))
            {
                channel = ChannelOptions.ChannelStomp;
            }
            else
            {
                channel = ChannelOptions.ChannelRest;
            }

            return Execute<Transaction>(transactionId, "start", type, null,
                new ChannelOptions { Channel = channel });
        }

        /// <summary>
        /// Cancel the existing (Loyalty) transaction with the given transactionId.
        /// </summary>
        /// <param name="transactionId">Cancel the specific transaction by id</param>
        /// <returns></returns>
        public bool Cancel(string transactionId)
        {
            return ExecuteToBool(transactionId, "cancel", null, null, null);
        }

        /// <summary>
        /// Starts extended Diagnose.
        /// </summary>
        /// <param name="transactionId">Starts the specific transaction by id</param>
        /// <returns></returns>
        public Transaction Diagnosis()
        {
            return Execute<Transaction>(null, "diagnosis", null, null, new ChannelOptions { Channel = ChannelOptions.ChannelStomp });
        }

        /// <summary>
        /// Starts End of Day Report (Kassenschnitt).
        /// </summary>
        /// <returns></returns>
        public Transaction EndOfDay()
        {
            // For EndOfDay STOMP is required
            var properties = Properties.GetDefault();

            if (properties.Get("Stomp.Enabled", false))
            {
                return Execute<Transaction>(null, "endOfDay", null, null, new ChannelOptions { Channel = ChannelOptions.ChannelStomp });
            }
            else
            {
                throw new System.Exception("For EndOfDay Stomp has to be enabled");
            }
        }

        /// <summary>
        /// Cancel payment transaction (different from Loyalty).
        /// </summary>
        /// <param name="receiptNumber">Receipt number to cancel</param>
        /// <returns></returns>
        public Transaction CancelPayment(string receiptNumber)
        {
            return Execute<Transaction>(receiptNumber, "cancelTrx", null, null, new ChannelOptions { Channel = ChannelOptions.ChannelStomp });
        }
    }
}