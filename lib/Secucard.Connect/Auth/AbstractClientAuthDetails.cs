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