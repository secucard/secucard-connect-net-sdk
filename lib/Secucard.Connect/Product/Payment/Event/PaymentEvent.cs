namespace Secucard.Connect.Product.Payment.Event
{
    using System;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment.Model;
    using Secucard.Connect.Client;

    public delegate void PaymentEventHandler(object sender, PaymentEventEventArgs args);

    public class PaymentEventEventArgs : EventArgs
    {
        public Event<SecupayCreditcard[]> SecucardEvent { get; set; }
    }
}