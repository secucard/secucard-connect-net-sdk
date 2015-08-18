/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Product.Smart
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
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

        protected override ServiceMetaData<Transaction> CreateMetaData()
        {
            return new ServiceMetaData<Transaction>("smart", "transactions");
        }

        private void OnNewEvent(object obj)
        {
            if (TransactionCashierEvent != null)
                TransactionCashierEvent(this,
                    new TransactionCashierEventArgs {SecucardEvent = (Event<Notification>) obj});
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