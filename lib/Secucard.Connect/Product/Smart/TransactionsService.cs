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
                    new TransactionCashierEventArgs {SecucardEvent = (Event<Notification>) obj});
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
                new ChannelOptions {Channel = channel });
        }

        /// <summary>
        /// Cancel the existing transaction with the given transactionId.
        /// </summary>
        /// <param name="transactionId">Cancel the specific transaction by id</param>
        /// <returns></returns>
        public bool Cancel(string transactionId)
        {
            return ExecuteToBool(transactionId, "cancel", null, null, null);
        }
    }
}