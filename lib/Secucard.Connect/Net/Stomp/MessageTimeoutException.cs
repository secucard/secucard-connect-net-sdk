namespace Secucard.Connect.Net.Stomp
{
    using System;

    /// <summary>
    ///     Indicates that a STOMP message was not received in time.
    ///     After a certain wait time this exception is thrown.
    /// </summary>
    public class MessageTimeoutException : Exception
    {
        public MessageTimeoutException(string message) : base(message)
        {
        }
    }
}