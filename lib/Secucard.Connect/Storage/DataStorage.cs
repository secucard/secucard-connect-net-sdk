namespace Secucard.Connect.Storage
{
    using System;
    using System.IO;

    /// <summary>
    ///     Gives basic access to the data store implementation.
    ///     Used to persist any data, like access tokens or downloaded media.
    /// </summary>
    [Serializable]
    public abstract class DataStorage
    {
        public abstract void Save(string id, object obj, bool replace);
        public abstract void Save(string id, Stream inStream, bool replace);

        public void Save(string id, object obj)
        {
            Save(id, obj, true);
        }

        public void Save(string id, Stream inStream)
        {
            Save(id, inStream, true);
        }

        public abstract object Get(string id);

        public T Get<T>(string id) where T : class
        {
            var obj = Get(id);
            return obj as T;
        }

        public abstract Stream GetStream(string id);
        public abstract void Clear(string id, long? ticks);

        public void Clear()
        {
            Clear("*", null);
        }

        /// <summary>
        ///     Removing all data older as the given ticks.
        /// </summary>
        public void Clear(long ticks)
        {
            Clear("*", ticks);
        }

        protected static bool WildCardMatch(string text, string pattern)
        {
            var cards = pattern.Split('*');
            foreach (var card in cards)
            {
                var idx = text.IndexOf(card, StringComparison.Ordinal);
                if (idx == -1)
                {
                    return false;
                }
                text = text.Substring(idx + card.Length);
            }
            return true;
        }
    }
}