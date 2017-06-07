namespace Secucard.Connect.Auth
{
    public class AuthCanceledException : System.Exception
    {
        public AuthCanceledException(string message) : base(message)
        {
        }
    }
}