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
        //public Transaction CreateTransaction(Transaction trans)
        //{
        //    return Create(trans);
        //}

        //public Transaction UpdateTransaction(Transaction trans)
        //{
        //    return Update(trans);
        //}

        public Transaction Start(string transactionId, string type)
        {
            // TODO: Call on STOMP
            return Execute<Transaction>(transactionId, "start", type, null, new ChannelOptions { Channel = ChannelOptions.CHANNEL_REST });
        }

        // TODO: Event onCashierDisplayChanged
    }
}
