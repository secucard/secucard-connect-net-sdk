namespace Secucard.Connect.Auth
{
    public sealed class UserCredentials
    {
        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("OAuthUserCredentials{{Username='{0}', Password='{1}'}}", Username, Password);
        }
    }
}