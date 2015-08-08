namespace Secucard.Stomp
{
    using System;

    public delegate void StompClientStatusChangedEventHandler(object sender, StompClientStatusChangedEventArgs args);

    public class StompClientStatusChangedEventArgs : EventArgs
    {
        public EnumStompClientStatus Status { get; set; }
        public DateTime Time { get; set; }
    }
}