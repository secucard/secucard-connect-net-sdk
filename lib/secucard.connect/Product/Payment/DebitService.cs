namespace Secucard.Connect.Product.General
{
    using Secucard.Model.Payment;

    public class DebitService : AbstractService
    {
        /// <summary>
        /// Create a new debit transaction
        /// </summary>
        public SecupayDebit CreateTransaction(SecupayDebit secupayDebit)
        {
            return Create(secupayDebit);
        }

        
    }
}
