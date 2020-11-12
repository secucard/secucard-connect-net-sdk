namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class PaymentTransactionsService : ProductService<PaymentTransaction>
    {
        public static readonly ServiceMetaData<PaymentTransaction> MetaData = new ServiceMetaData<PaymentTransaction>("payment",
            "transactions");

        public Transaction GetOldFormat(string id)
        {
            return this.GetWithAction<Transaction>(id, "oldFormat", null, null, null);
        }

        public CrowdFundingData GetCrowdFundingData(string merchantId)
        {
            return this.GetWithAction<CrowdFundingData>("not_set", "crowdfundingdata", merchantId, null, null);
        }

        // Assign a incoming payment to a (prepay) payment transaction
        public AssignedPaymentTransaction AssignPayment(string paymentId, string accountingId)
        {
            return this.Execute<AssignedPaymentTransaction>(paymentId, "assignPayment", accountingId, null, null);
        }

        // Cancel or Refund an existing transaction.
        public bool Cancel(string paymentId, string contractId = null, string amount = null, bool reduceStakeholderPayment = false)
        {
            object obj = new {
                contract = contractId,
                amount,
                reduce_stakeholder_payment = reduceStakeholderPayment,
                return_old_format = true
            };

            return this.ExecuteToBool(paymentId, "cancel", null, obj, null);
        }

        // Capture a pre-authorized payment transaction.
        public bool Capture(string paymentId, string contractId = null)
        {
            object obj = new
            {
                contract = contractId
            };

            return this.ExecuteToBool(paymentId, "capture", null, obj, null);
        }

        // Add additional basket items to the payment transaction. F.e. for adding stakeholder payment items.
        public bool UpdateBasket(string paymentId, Basket[] basket, string contractId = null)
        {
            object obj = new Transaction
            {
                Id = paymentId,
                Basket = basket,
                Contract = new Contract { Id = contractId }
            };

            return this.UpdateToBool(paymentId, "basket", null, obj, null);
        }

        // Remove the accrual flag of an existing payment transaction.
        public bool ReverseAccrual(string paymentId)
        {
            return this.ExecuteToBool(paymentId, "revokeAccrual", null, null, null);
        }

        // Add some shipping information, like the shipping provider (carrier) or a tracking number for the parcel.
        public bool SetShippingInformation(string paymentId, string carrier, string trackingId, string invoiceNumber = null)
        {
            object obj = new
            {
                carrier,
                tracking_id = trackingId,
                invoice_number = invoiceNumber
            };

            return this.UpdateToBool(paymentId, "shippingInformation", null, obj, null);
        }

        protected override ServiceMetaData<PaymentTransaction> GetMetaData()
        {
            return MetaData;
        }
    }
}