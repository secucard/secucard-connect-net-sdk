namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

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
