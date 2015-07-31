namespace Secucard.Connect.Product.General
{
    using Secucard.Model;
    using Secucard.Model.Smart;

    public class SmartIdentsService: AbstractService
    {
        public ObjectList<Ident> GetIdents()
        {
            return GetList<Ident>(null);
        }
    }
}
