namespace Secucard.Connect.Net.Stomp.Client
{
    using System;

    /// <summary>
    ///     Indicates that no receipt for a message was received in time.
    /// </summary>
    public class NoReceiptException : Exception
    {
        public NoReceiptException()
        {
        }

        public NoReceiptException(string message) : base(message)
        {
        }
    }
}