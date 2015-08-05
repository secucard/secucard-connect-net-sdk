namespace secucard.connect.test
{
    using Secucard.Connect.auth;
    using Secucard.Connect.auth.Model;
    using Secucard.Connect.Auth;

    public class ClientAuthDetailsUserTest: AbstractClientAuthDetails,IClientAuthDetails
    {
        public OAuthCredentials GetCredentials()
        {
            return new AppUserCredentials(GetClientCredentials() ,"f0478f73afe218e8b5f751a07c978ecf", "30644327cfbde722ad2ad12bb9c0a2f86a2bee0a2d8de8d862210112af3d01bb", "device");
        }

        public ClientCredentials GetClientCredentials()
        {
            return new ClientCredentials(
            "f0478f73afe218e8b5f751a07c978ecf",
            "30644327cfbde722ad2ad12bb9c0a2f86a2bee0a2d8de8d862210112af3d01bb");
        }
    }
}
