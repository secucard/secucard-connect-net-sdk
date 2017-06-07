namespace Secucard.Connect.Auth.Exception
{
    using System;
    using Secucard.Connect.Client;

    /// <summary>
    /// Indicates an authorization attempt failed due missing or invalid authentication data.
    ///  Typically this kind of error is caused by wrong API usage or alike, something that is wrong implemented.
    /// </summary>
    public class AuthFailedException : AuthError
    {
        private string _error;

        public string GetError()
        {
            return _error;
        }

        public void SetError(string error)
        {
            _error = error;
        }

        public AuthFailedException(string message, string error)
            : base(message)
        {
            _error = error;
        }

        public AuthFailedException(string message)
            : base(message)
        {
        }

        public AuthFailedException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}