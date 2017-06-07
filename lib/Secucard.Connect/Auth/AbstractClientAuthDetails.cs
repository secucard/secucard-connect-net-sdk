namespace Secucard.Connect.Auth
{
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Storage;

    /// <summary>
    /// Abstract implementation which just delegates the token persistence to a memory based cache.
    /// </summary>
    public abstract class AbstractClientAuthDetails
    {
        private readonly DataStorage _storage;

        public AbstractClientAuthDetails()
        {
            _storage = new MemoryDataStorage();
        }

        public Token GetCurrent()
        {
            return (Token) _storage.Get("token");
        }

        public void OnTokenChanged(Token token)
        {
            _storage.Save("token", token);
        }
    }
}