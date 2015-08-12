namespace Secucard.Connect.Product.Smart
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Smart.Model;

    public class SmartIdentsService : ProductService<Ident>
    {
        protected override ServiceMetaData<Ident> CreateMetaData()
        {
            return new ServiceMetaData<Ident>("smart", "idents");
        }
    }
}
