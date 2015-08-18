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

namespace Secucard.Connect.Product.Smart
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Smart.Model;

    public class IdentsService : ProductService<Ident>
    {
        public static readonly ServiceMetaData<Ident> META_DATA = new ServiceMetaData<Ident>("smart", "idents");

        protected override ServiceMetaData<Ident> GetMetaData()
        {
            return META_DATA;
        }

        /// <summary>
        ///     Read an ident with a given id from a connected device.
        /// </summary>
        public Ident ReadIdent(string id)
        {
            return Execute<Ident>(id, "read", null, null, null);
        }
    }
}