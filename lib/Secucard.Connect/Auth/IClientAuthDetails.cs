namespace Secucard.Connect.Auth
{
    using Secucard.Connect.Auth.Model;

    /// <summary>
    /// Defines operations to access the necessary authentication details provided by the client which request authentication.
    /// Administration of confidential data is delegated to the client through this interface.
    /// </summary>
    public interface IClientAuthDetails
    {
        /// <summary>
        /// Returns the credentials needed to obtain an new access token and refresh token.
        ///  The returned type depends on the authentication type used with the client. Should never return null.
        /// </summary>
        OAuthCredentials GetCredentials();

        /// <summary>
        ///   Returns the client credentials needed to obtained an access token with an existing refresh token. Should never return null.
        /// </summary>
        ClientCredentials GetClientCredentials();

        /// <summary>
        /// Returns the current Oauth token data passed by {@link #onTokenChanged(com.secucard.connect.auth.model.Token)}  or null if no token is available yet.
        /// </summary>
        Token GetCurrent();

        /// <summary>
        /// Called when a new token was obtained. The client must persist the given token in a way that calls to {@link #getCurrent()} can return this token anytime.
        /// </summary>
        void OnTokenChanged(Token token);
    }
}