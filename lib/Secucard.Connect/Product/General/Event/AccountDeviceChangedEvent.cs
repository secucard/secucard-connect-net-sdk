namespace Secucard.Connect.Product.General.Event
{
    using System;
    using Secucard.Connect.Product.General.Model;

    public delegate void AccountDeviceChangedEventHandler(object sender, AccountDeviceChangedEventArgs args);

    public class AccountDeviceChangedEventArgs : EventArgs
    {
        public Event<AccountDevice> SecucardEvent { get; set; }
    }
}