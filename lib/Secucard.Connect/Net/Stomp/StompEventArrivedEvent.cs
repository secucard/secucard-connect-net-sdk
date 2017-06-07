namespace Secucard.Connect.Net.Stomp
{
    using System;

    public delegate void StompEventArrivedEventHandler(object sender, StompEventArrivedEventArgs args);

    public class StompEventArrivedEventArgs : EventArgs
    {
        public string Body { get; set; }
        public DateTime Time { get; set; }
    }
}