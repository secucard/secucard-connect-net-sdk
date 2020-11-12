namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    [System.Obsolete("Not used any more")]
    public class GeneralTransactionsService : ProductService<Transaction>
    {
        public static readonly ServiceMetaData<Transaction> MetaData = new ServiceMetaData<Transaction>("general",
            "transactions");

        protected override ServiceMetaData<Transaction> GetMetaData()
        {
            return MetaData;
        }
    }
}