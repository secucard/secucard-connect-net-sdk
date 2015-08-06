namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class AccountsService : ProductService<Account>
    {

        protected override ServiceMetaData<Account> CreateMetaData()
        {
            return new ServiceMetaData<Account>("general", "accounts");
        }
    }
}
