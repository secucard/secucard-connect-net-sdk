namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Event;
    using Secucard.Connect.Product.General.Event;
    using Secucard.Connect.Product.General.Model;

    [System.Obsolete("Not used any more")]
    public class AccountDevicesService : ProductService<AccountDevice>
    {
        public static readonly ServiceMetaData<AccountDevice> MetaData = new ServiceMetaData<AccountDevice>("general",
            "accountdevices");

        protected override ServiceMetaData<AccountDevice> GetMetaData()
        {
            return MetaData;
        }

        public AccountDeviceChangedEventHandler AccountDeviceChangedEvent;


        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, GetMetaData().ProductResource, Events.TypeChanged,
                OnNewChangedEvent);
        }

        private void OnNewChangedEvent(object obj)
        {
            if (AccountDeviceChangedEvent != null)
                AccountDeviceChangedEvent(this,
                    new AccountDeviceChangedEventArgs {SecucardEvent = (Event<AccountDevice>) obj});
        }
    }
}