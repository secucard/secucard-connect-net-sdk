namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class SecupayPayout : SecuObject
    {
        [DataMember(Name = "demo")]
        public bool Demo { get; set; }

        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "customer")]
        public string Customer { get; set; }

        [DataMember(Name = "order_id")]
        public string OrderId { get; set; }

        [DataMember(Name = "purpose")]
        public string Purpose { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "transaction_status")]
        public string TransactionStatus { get; set; }

        [DataMember(Name = "trans_id")]
        public string TransId { get; set; }

        [DataMember(Name = "transaction_list")]
        public TransactionList[] TransactionList { get; set; }

        /// <summary>
        /// If TRUE the payment transaction will be only a pre-authorization
        /// and a separate capture or cancel is needed to start the payment processing
        /// </summary>
        [DataMember(Name = "accrual")]
        public bool Accrual { get; set; }

        [System.ObsoleteAttribute()]
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
        [System.ObsoleteAttribute()]
        [DataMember(Name = "opt_data")]
        public OptData OptData { get; set; }
        
        [DataMember(Name = "transfer_purpose")]
        public string TransferPurpose { get; set; }

        [DataMember(Name = "transfer_account")]
        public TransferAccount TransferAccount { get; set; }

        public override string ToString()
        {
            return "SecupayPayout{" +
                   "Amount=" + this.Amount +
                   "Contract=" + this.Contract +
                   "Currency=" + this.Currency +
                   "Customer=" + this.Customer +
                   "OrderId=" + this.OrderId +
                   "Purpose=" + this.Purpose +
                   "Status=" + this.Status +
                   "TransactionStatus=" + this.TransactionStatus +
                   "TransId=" + this.TransId +
                   "TransactionList=" + this.TransactionList +
                   "Accrual=" + this.Accrual +
                   "Subscription=" + this.Subscription +
                   "RedirectUrl=" + this.RedirectUrl +
                   "OptData=" + this.OptData +
                   "transferPurpose='" + this.TransferPurpose + '\'' +
                   ", transferAccount=" + TransferAccount +
                   "} " + base.ToString();
        }
    }
}