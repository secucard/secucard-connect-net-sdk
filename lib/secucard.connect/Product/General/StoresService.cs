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
namespace Secucard.Connect.Product.General
{
    using System.Collections.Generic;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    public class StoresService : ProductService<Store>
    {
        protected override ServiceMetaData<Store> CreateMetaData()
        {
            return new ServiceMetaData<Store>("general", "store");
        }

        /// <summary>
        /// Check in the store with the given id.
        /// </summary>
        public bool CheckIn(string storeId)
        {
            return ExecuteToBool(storeId, "checkin", null, null,null);
        }
        
        /// <summary>
        /// Check out of the store with the given id.
        /// </summary>
        public bool CheckOut(string storeId)
        {
            return ExecuteToBool(storeId, "checkin", "false", null, null);
        }

        /// <summary>
        /// Set store with given id as default.
        /// </summary>
        public bool SetDefault(string storeId)
        {
            return ExecuteToBool(storeId, "setDefault", null, null, null);
        }

        public new ObjectList<Store> GetList(QueryParams queryParams)
        {
            var list = base.GetList(queryParams);
            ProcessStore(list.List);
            return list;
        }

        /// <summary>
        /// Post processing to retrieve image data
        /// </summary>
        private static void ProcessStore(List<Store> stores)
        {
            foreach (var obj in stores)
            {
                MediaResource picture = obj.logo;
                if (picture != null)
                {
                    if (!picture.IsCached)
                    {
                        picture.Download();
                    }
                }
            }
        }
    }
}