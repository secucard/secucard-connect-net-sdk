namespace Secucard.Connect.Net.Stomp.Client
{
    using System;

    public delegate void StompCoreExceptionEventHandler(object sender, StompCoreExceptionEventArgs args);

    public class StompCoreExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public DateTime Time { get; set; }
    }
}