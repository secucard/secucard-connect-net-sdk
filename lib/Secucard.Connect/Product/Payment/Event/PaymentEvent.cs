namespace Secucard.Connect.Product.Payment.Event
{
    using System;
    using Secucard.Connect.Product.General.Model;

    public delegate void PaymentEventHandler<T>(object sender, PaymentEventEventArgs<T> args);

    public class PaymentEventEventArgs<T> : EventArgs
    {
        public Event<T[]> SecucardEvent { get; set; }
    }
}