namespace Secucard.Connect.Client
{
    using System;

    /// <summary>
    /// Indicates a general internal error happened. Usually this kind or error is caused by unexpected, unusual conditions and is  most likely not recoverable.
    /// </summary>
    public class ClientError : Exception
    {
        public ClientError(string message)
            : base(message)
        {
        }

        public ClientError(string message, Exception ex) : base(message, ex)
        {
        }
    }
}