namespace Secucard.Connect.Auth
{
    using Secucard.Model.Auth;

    /// <summary>
    ///     Provides a interface for getting authorization tokens.
    /// </summary>
    public interface IAuthProvider
    {
        /**
         * Requesting a authorization token.
         * The registered event listener may receive events during the process.
         */


        AuthToken GetToken();
        AuthToken GetToken(bool extendToken);
        /**
         * Cancel a pending authorization request. Only useful for auth processes which involves token polling step.
         * Provider throws {@link com.Secucard.Connect.Auth.AuthCanceledException} if successfully canceled.
         */
        void CancelAuth();
        /**
         * Registering a listener getting events occurring during the auth process. This is the case when a auth.
         * process is done in multiple steps and user input is required.
         */
        //void registerEventListener(EventListener eventListener);

        /**
         * Removes all stored access tokens from clients cache.
         */
        void ClearAuthCache();
    }
}