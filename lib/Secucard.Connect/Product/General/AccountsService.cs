namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class AccountsService : ProductService<Account>
    {
        public static readonly ServiceMetaData<Account> MetaData = new ServiceMetaData<Account>("general", "accounts");

        protected override ServiceMetaData<Account> GetMetaData()
        {
            return MetaData;
        }
    }
}