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

namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public abstract class Transaction : SecuObject
    {
        /// <summary>
        /// status for accepted debit transactions and finished prepay transactions
        /// </summary>
        public const string STATUS_ACCEPTED = "accepted";

        /// <summary>
        /// prepay transaction after creation , before payment arrives
        /// </summary>
        public const string STATUS_AUTHORIZED = "authorized";

        /// <summary>
        /// when scoring for debit transaction denies the payer
        /// </summary>
        public const string STATUS_DENIED = "denied";

        /// <summary>
        /// then ruecklastschrift happens, or some other issue type
        /// </summary>
        public const string STATUS_ISSUE = "issue";

        /// <summary>
        /// when transaction is cancelled by creator (it is not possible to cancel transactions any time, so the debit transaction is possible to cancel until it is cleared out)
        /// </summary>
        public const string STATUS_VOID = "void";

        /// <summary>
        /// when issue for transaction is resolved
        /// </summary>
        public const string STATUS_ISSUE_RESOLVED = "issue_resolved";

        /// <summary>
        /// special status, saying that transaction was paid back (for some reason)
        /// </summary>
        public const string STATUS_REFUND = "refund";

        /// <summary>
        /// should not happen, but only when status would be empty, this status is used
        /// </summary>
        public const string STATUS_INTERNAL_SERVER_STATUS = "internal_server_status";

        /// <summary>
        /// Use the Authorization option to place a hold on the payer funds
        /// </summary>
        public const string PAYMENT_ACTION_AUTHORIZATION = "authorization";

        /// <summary>
        /// Direct payment (immediate debit of the funds from the buyer's funding source)
        /// </summary>
        public const string PAYMENT_ACTION_SALE = "sale";

        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "recipient")]
        public Customer Recipient { get; set; }

        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        [DataMember(Name = "purpose")]
        public string Purpose { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "transaction_status")]
        public string TransactionStatus { get; set; }

        [DataMember(Name = "transId")]
        public string TransId { get; set; }

        [DataMember(Name = "basket")]
        public Basket[] Basket { get; set; }

        [DataMember(Name = "experience")]
        public Experience Experience { get; set; }

        /// <summary>
        /// If TRUE the payment transaction will be only a pre-authorization
        /// and a separate capture or cancel is needed to start the payment processing
        /// </summary>
        [DataMember(Name = "accrual")]
        public bool Accrual { get; set; }

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }

        /// <summary>
        /// Redirect urls used for the payment checkout page
        /// </summary>
        [DataMember(Name = "redirect_url")]
        public RedirectUrl RedirectUrl { get; set; }

        /// <summary>
        /// Optional settings and parameters to customize the checkout process
        /// </summary>
        [DataMember(Name = "opt_data")]
        public OptData OptData { get; set; }

        /// <summary>
        /// The "payment_action" parameter controls the processing of the transaction by secupay, for the time being,
        /// there are the values "sale" and "authorization". Sale is a direct payment.
        /// To perform the transaction later, you have to transmit "authorization" here.
        /// </summary>
        [DataMember(Name = "payment_action")]
        public string PaymentAction { get; set; } = PAYMENT_ACTION_SALE;
    }
}