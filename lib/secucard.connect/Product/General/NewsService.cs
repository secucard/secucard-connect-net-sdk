﻿/*
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
    using System.Threading.Tasks;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    public class NewsService : ProductService<News>
    {
        protected override ServiceMetaData<News> CreateMetaData()
        {
            return new ServiceMetaData<News>("general", "news");
        }

        /// <summary>
        ///  Mark news with given id as read.  
        ///  return True if successfully updated, false else
        /// </summary>
        public bool MarkRead(string id)
        {
            return ExecuteToBool(id, "markRead", null, null, null);
        }

        public new ObjectList<News> GetList(QueryParams queryParams)
        {
            var list = base.GetList(queryParams);
            PostProcess(list.List);
            return list;
        }

        public new News Get(string storeId)
        {
            var news = base.Get(storeId);
            PostProcess(new List<News> { news });
            return news;
        }

        /// <summary>
        /// Post processing to retrieve image data
        /// </summary>
        private static void PostProcess(IEnumerable<News> list)
        {
            Parallel.ForEach(list, obj =>
            {
                var mediaResource = obj.PictureObject;
                if (mediaResource != null)
                {
                    if (!mediaResource.IsCached)
                    {
                        mediaResource.Download();
                    }
                }
            });
        }
    }
}