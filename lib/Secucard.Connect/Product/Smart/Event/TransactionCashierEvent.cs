namespace Secucard.Connect.Product.Smart.Event
{
    using System;
    using Secucard.Connect.Product.General.Model;

    public delegate void TransactionCashierEventHandler(object sender, TransactionCashierEventArgs args);

    public class TransactionCashierEventArgs : EventArgs
    {
        public Event<Notification> SecucardEvent { get; set; }
    }
}