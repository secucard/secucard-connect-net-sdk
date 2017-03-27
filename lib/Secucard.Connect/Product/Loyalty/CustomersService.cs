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

namespace Secucard.Connect.Product.Loyalty
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Loyalty.Model;
    using System.Linq;
    using General.Model;

    public class CustomerLoyaltyService : ProductService<Customer>
    {
        public static readonly ServiceMetaData<Customer> MetaData = new ServiceMetaData<Customer>("loyalty",
            "customers");

        protected override ServiceMetaData<Customer> GetMetaData()
        {
            return MetaData;
        }

        public new ObjectList<Customer> GetList(QueryParams queryParams)
        {
            var list = base.GetList(queryParams);
            PostProcess(list.List);
            return list;
        }

        public new Customer Get(string customerId)
        {
            var store = base.Get(customerId);
            PostProcess(new List<Customer> {store});
            return store;
        }

        /// <summary>
        /// Post processing to retrieve image data
        /// </summary>
        private static void PostProcess(IEnumerable<Customer> list)
        {
            Parallel.ForEach(list, obj =>
            {
                // Actually there are 3 "Contact" attributes in "Customer" which contains picture object
                foreach (var pictureAttribute in obj.GetType().GetFields().Where(p => p.GetType() == typeof(Contact)))
                {
                    var mediaResource = (MediaResource)pictureAttribute.GetValue(obj);
                    if (mediaResource != null && !mediaResource.IsCached)
                    {
                        mediaResource.Download();
                    }
                }
            });
        }
    }
}