namespace Secucard.Connect.Auth.Model
{
    public abstract class OAuthCredentials
    {
        /// <summary>
        ///     Returns an id which uniquely identifies this instance in a way that same ids refer to the same credentials.
        /// </summary>
        public abstract string Id { get; }
    }
}