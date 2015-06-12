namespace Secucard.Connect.Auth
{
    using System;

    public class AuthCanceledException : Exception
    {
        public AuthCanceledException(string message) : base(message)
        {
        }
    }
}