namespace Secucard.Connect.Auth.Model
{
    public class AnonymousCredentials : OAuthCredentials
    {
        public override string Id
        {
            get { return "anonymous"; }
        }
    }
}