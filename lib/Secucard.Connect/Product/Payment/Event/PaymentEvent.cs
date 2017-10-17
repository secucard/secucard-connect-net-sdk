namespace Secucard.Connect.Product.Payment.Event
{
    using Secucard.Connect.Product.General.Model;
    using System;

    public delegate void PaymentEventHandler<T>(object sender, PaymentEventEventArgs<T> args);

    public class PaymentEventEventArgs<T> : EventArgs
    {
        public Event<T[]> SecucardEvent { get; set; }
    }
}