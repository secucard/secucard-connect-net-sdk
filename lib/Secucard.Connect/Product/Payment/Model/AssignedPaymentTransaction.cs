namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class AssignedPaymentTransaction
    {
        // The remaining amount of the incoming payment
        // (read only)
        [DataMember(Name = "remaining_payment_amount")]
        public int RemainingPaymentAmount { get; set; }

        // The remaining (open) amount of the payment transaction
        // (read only)
        [DataMember(Name = "remaining_transaction_amount")]
        public int RemainingTransactionAmount { get; set; }

        // The remaining amount of the incoming payment before the assigment
        // (read only)
        [DataMember(Name = "remaining_payment_amount_before")]
        public int RemainingPaymentAmountBefore { get; set; }

        // The remaining (open) amount of the payment transaction before the assigment
        // (read only)
        [DataMember(Name = "remaining_transaction_amount_before")]
        public int RemainingTransactionAmountBefore { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return "AssignedPaymentTransaction{" +
                   "remaining_payment_amount=" + this.RemainingPaymentAmount +
                   "remaining_transaction_amount=" + this.RemainingTransactionAmount +
                   "remaining_payment_amount_before=" + this.RemainingPaymentAmountBefore +
                   "remaining_transaction_amount_before=" + this.RemainingTransactionAmountBefore +
                   '}';
        }
    }
}