namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class AccountDevicesService : ProductService<AccountDevice>
    {

        protected override ServiceMetaData<AccountDevice> CreateMetaData()
        {
            return new ServiceMetaData<AccountDevice>("general", "accountdevices");
        }
    }
}
