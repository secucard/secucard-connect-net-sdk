namespace Secucard.Connect.Client
{
    using System;

    public class SecucardConnectException : Exception
    {
        public SecucardConnectException(string message) : base(message)
        {
        }
    }
}