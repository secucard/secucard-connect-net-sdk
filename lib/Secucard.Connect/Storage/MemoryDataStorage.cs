/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
    public class MemoryDataStorage : DataStorage
    {
        private readonly Dictionary<string, StorageItem> Store = new Dictionary<string, StorageItem>();

        public override void Save(string id, object obj, bool replace)
        {
            SaveInternal(id, obj, replace);
        }

        public override void Save(string id, Stream inStream, bool replace)
        {
            SaveInternal(id, inStream, replace);
        }

        public override object Get(string id)
        {
            return GetInternal(id);
        }

        public override Stream GetStream(string id)
        {
            return (Stream) GetInternal(id);
        }

        public override void Clear(string id, long? ticks)
        {
            if (id == null) return;

            if ("*".Equals(id) && ticks == null)
            {
                Store.Clear();
                return;
            }

            foreach (var key in Store.Keys.ToList())
            {
                if (WildCardMatch(key, id))
                {
                    var item = Store[key];
                    if (ticks == null || item.Ticks == null || item.Ticks < ticks)
                    {
                        Store.Remove(key);
                    }
                }
            }
        }

        public int Size()
        {
            return Store.Count;
        }

        private bool SaveInternal(string id, object obj, bool replace)
        {
            if (!replace && Store.ContainsKey(id))
            {
                return false;
            }
            StorageItem item;
            if (Store.ContainsKey(id))
                item = Store[id];
            else
            {
                item = new StorageItem();
                Store.Add(id, item);
            }

            var stream = obj as Stream;
            if (stream != null)
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                item.Type = "is";
                item.Value = ms.ToArray();
            }
            else
            {
                item.Type = null;
                item.Value = obj;
            }
            item.Ticks = DateTime.Now.Ticks;

            return true;
        }

        private object GetInternal(string id)
        {
            if (!Store.ContainsKey(id)) return null;

            var item = Store[id];
            if (item == null) return null;

            if ("is".Equals(item.Type))
            {
                return new MemoryStream((byte[]) item.Value);
            }

            return item.Value;
        }

        public byte[] Serialize()
        {
            var formatter = new BinaryFormatter();
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        public void SaveToFile(string fullFileName)
        {
            File.WriteAllBytes(fullFileName, Serialize());
        }

        public static MemoryDataStorage Deserialize(MemoryStream ms)
        {
            MemoryDataStorage storage;

            try
            {
                var formatter = new BinaryFormatter();
                storage = (MemoryDataStorage) formatter.Deserialize(ms);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }

            return storage;
        }

        public static MemoryDataStorage LoadFromFile(string fullFileName)
        {
            if (File.Exists(fullFileName))
                return Deserialize(new MemoryStream(File.ReadAllBytes(fullFileName)));
            return new MemoryDataStorage();
        }
    }
}