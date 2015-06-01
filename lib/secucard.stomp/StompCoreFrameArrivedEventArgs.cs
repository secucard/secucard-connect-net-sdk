namespace secucard.stomp
{
    using System;

    public class StompCoreFrameArrivedEventArgs : EventArgs
    {
        public StompFrame Frame { get; set; }
        public DateTime Time { get; set; }
    }
}