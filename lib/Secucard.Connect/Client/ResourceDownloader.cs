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

namespace Secucard.Connect.Client
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Storage;

    /// <summary>
    /// Retrieves resources via HTTP and stores in local cache. Later requests are served from the cac
    /// </summary>
    public class ResourceDownloader
    {
        private static string INVALID_CHARS_PATTERN = "[\\/:*?\"<>|\\.&]+";

        /// <summary>
        /// Singelton Pattern
        /// </summary>
        private static readonly ResourceDownloader Instance = new ResourceDownloader();

        private bool retry = false;
        internal RestChannel RestChannel { get; set; }
        internal DataStorage Cache { get; set; }

        private ResourceDownloader()
        {
        }

        public static ResourceDownloader Get()
        {
            return Instance;
        }

        private static string CreateId(string url)
        {
            var regex = new Regex(INVALID_CHARS_PATTERN);
            var s = regex.Replace(url, string.Empty);
            if (s.Length > 120)
            {
                s = s.Substring(0, 120);
            }
            return s;
        }

        /// <summary>
        /// Retrieve a resource and store in cache. Overrides existing resources with same URL.
        /// </summary>
        internal void Download(string url)
        {
            Stream stream;
            int count = 0;
            Exception ex = null;
            do
            {
                try
                {
                    stream = GetStream(url);
                }
                catch (Exception e)
                {
                    // TODO: check out which exception are subject to retry, retry is disabled until
                    stream = null;
                    ex = e;
                }
            } while (retry && stream == null && count++ < 2);

            if (ex != null)
            {
                throw new ClientError("Unable to download resource from " + url, ex);
            }

            if (stream != null)
            {
                Cache.Save(CreateId(url), stream);
            }
        }

        /// <summary>
        /// Returns an input stream to a resource to read from. 
        ///  The resources contents will be cached during this. Later access is served from cache.
        /// </summary>
        internal Stream GetInputStream(string url, bool useCache)
        {
            Stream stream;
            if (useCache)
            {
                string id = CreateId(url);
                stream = Cache.GetStream(id);
                if (stream == null)
                {
                    Download(url);
                    stream = Cache.GetStream(id);
                }
            }
            else
            {
                stream = GetStream(url);
            }
            return stream;
        }

        private Stream GetStream(string url)
        {
            RestRequest request = new RestRequest() {Method = "GET", Url = url};
            return RestChannel.GetStream(request);
        }
    }
}