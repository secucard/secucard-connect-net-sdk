namespace Secucard.Connect.Auth.Exception
{
    using Secucard.Connect.Client;

    /// <summary>
    /// Indicates an authentication is denied due wrong credentials. All needed data are present but
    ///  This kind of error could usually be resolved by trying again with correct data.
    /// </summary>
    public class AuthDeniedException : AuthError
    {
        public AuthDeniedException()
        {
        }

        public AuthDeniedException(string message) : base(message)
        {
        }
    }
}