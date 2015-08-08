namespace Secucard.Stomp
{
    using System;

    public delegate void StompCoreFrameArrivedEventHandler(object sender, StompCoreFrameArrivedEventArgs args);

    public class StompCoreFrameArrivedEventArgs : EventArgs
    {
        public StompFrame Frame { get; set; }
        public DateTime Time { get; set; }
    }
}