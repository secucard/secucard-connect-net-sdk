namespace Secucard.Connect.Storage
{
    using System;
    using System.IO;

    /// <summary>
    ///     Gives basic access to the data store implementation.
    ///     Used to persist any data, like acess tokens or downloaded media.
    /// </summary>
    [Serializable]
    public abstract class DataStorage
    {
        /**
   * Save a object for an id.
   * Must handle also InputStream instances provided as input.
   *
   * @param id      An unique id of the object.
   * @param object  The object to save.
   * @param replace True if a existing object of same id should be overwritten, false else.
   * @throws DataStorageException if a error ocurrs.
   */
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

        /**
         * Returns the stored object for the provided id.
         * May return InputStream instances, in this case the caller is responsible for closing
         *
         * @param id Id of the object to get.
         * @return The stored content or null if nothing available.
         */
        public abstract object Get(string id);

        public T Get<T>(string id) where T : class
        {
            var obj = Get(id);
            return obj as T;
        }

        public abstract Stream GetStream(string id);
        /**
         */
        /**
         * Remove all data for a given id which are older as a given timestamp.
         * The id may contain a wildcard sign "*" - in this case all matching entries are removed.
         *
         * @param id        The id.
         * @param timestampMs A Unix timestamp in ms, null to omit.
         */
        public abstract void Clear(string id, long? ticks);
        /**
         * Removing all data.
         */

        public void Clear()
        {
            Clear("*", null);
        }

        /// <summary>
        ///     Removing all data older as the given ticks.
        /// </summary>
        /// <param name="ticks"></param>
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