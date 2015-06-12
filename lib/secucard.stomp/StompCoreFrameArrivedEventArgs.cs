namespace Secucard.Stomp
{
    using System;

    public class StompCoreFrameArrivedEventArgs : EventArgs
    {
        public StompFrame Frame { get; set; }
        public DateTime Time { get; set; }
    }
}