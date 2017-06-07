namespace Secucard.Connect.Product.Smart.Event
{
    using System;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Smart.Model;

    public delegate void CheckinEventHandler(object sender, CheckinEventEventArgs args);

    public class CheckinEventEventArgs : EventArgs
    {
        public Event<Checkin> SecucardEvent { get; set; }
    }
}