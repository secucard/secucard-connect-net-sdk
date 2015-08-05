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

namespace Secucard.Connect.auth.Model
{
    using System.Collections.Generic;

    public abstract class OAuthCredentials
    {
        public abstract string GrantType { get; }
        /**
   * Returns an id which uniquely identifies this instance in a way that same ids refer to the same credentials.
   *
   * @return The id as string.
   */
        public abstract string Id { get; }

        public virtual Dictionary<string, object> AsMap()
        {
            var map = new Dictionary<string, object> {{"grant_type", GrantType}};
            return map;
        }
    }
}