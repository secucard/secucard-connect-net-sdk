namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Model;
    using Secucard.Model.Smart;

    public class SmartIdentsService : ProductService<Ident>
    {
        protected override ServiceMetaData<Ident> CreateMetaData()
        {
            return new ServiceMetaData<Ident>("payment", "debits");
        }


        //public ObjectList<Ident> GetIdents()
        //{
        //    return GetList<Ident>(null);
        //}
    }
}
