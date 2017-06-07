namespace Secucard.Connect.Client
{
    using System;

    /// <summary>
    /// Indicates that an authentication problem happened during an operation.
    /// Inspect the actual exception type to get details about the cause. Some type are recoverable just by correcting user input.
    /// </summary>
    public class AuthError : Exception
    {
        public AuthError()
        {
        }

        public AuthError(string message)
            : base(message)
        {
        }

        public AuthError(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}