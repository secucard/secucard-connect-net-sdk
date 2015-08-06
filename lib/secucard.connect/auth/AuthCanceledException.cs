namespace Secucard.Connect.auth
{
    public class AuthCanceledException : System.Exception
    {
        public AuthCanceledException(string message) : base(message)
        {
        }
    }
}