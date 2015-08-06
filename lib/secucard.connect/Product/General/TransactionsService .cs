namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class TransactionsService : ProductService<Transaction>
    {
        protected override ServiceMetaData<Transaction> CreateMetaData()
        {
            return new ServiceMetaData<Transaction>("general", "transaction");
        }
    }
}