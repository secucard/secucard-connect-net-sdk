namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Model.Payment;

    public class DebitService : ProductService<SecupayDebit>
    {
        protected override ServiceMetaData<SecupayDebit> CreateMetaData()
        {
            return new ServiceMetaData<SecupayDebit>("payment", "debits");
        }

        ///// <summary>
        ///// Create a new debit transaction
        ///// </summary>
        //public SecupayDebit CreateTransaction(SecupayDebit secupayDebit)
        //{
        //    return Create(secupayDebit);
        //}

        
    }
}
