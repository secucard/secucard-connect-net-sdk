namespace Secucard.Connect.Product.General
{
    using System.Collections.Generic;
    using Secucard.Model.Smart;

    public class SmartTransactionsService : AbstractService
    {

        public Transaction CreateTransaction(Transaction trans)
        {
            return Create(trans);
        }

        public Transaction UpdateTransaction(Transaction trans)
        {
            return Update(trans);
        }

        public Transaction StartTransaction(string transactionId, string type)
        {
            // TODO: Call on STOMP
            return Execute<Transaction, object>(transactionId, "start", new List<string> { type }, null);
        }

        // TODO: Event onCashierDisplayChanged
    }
}
