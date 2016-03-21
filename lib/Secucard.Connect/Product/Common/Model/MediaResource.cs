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

namespace Secucard.Connect.Product.Common.Model
{
    using System;
    using System.IO;
    using Secucard.Connect.Client;

    /// <summary>
    /// Base class for all URL based media resources like images or pdf documents.
    /// Supports caching of the resource denoted by the URL of this instance. That means the content is downloaded and 
    /// put to the cache on demand. Further access is served by the cache.<br/>
    /// Note: This is not a caching by LRU strategy or alike. If enabled the content will be
    /// cached for new instances or when the URL of the instance was changed (its eventually the same).
    /// </summary>
    public class MediaResource
    {
        private string _url;

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                IsCached = false;
            }
        }

        /// <summary>
        /// Returns if this resource was already downloaded and cached.  Note: If this instances URL is changed the flag is reset.
        /// </summary>
        public bool IsCached { get; set; }

        /// <summary>
        /// Set if caching is enabled or not. Default is enabled.
        /// </summary>
        private bool CachingEnabled { get; set; }

        public MediaResource()
        {
            CachingEnabled = true;
        }

        public static MediaResource Create(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    return new MediaResource(url);
                }
                catch (Exception e)
                {
                    SecucardTrace.Error("MediaResource.Create", "Url= {0},{1} ", url, e.Message);
                    // ignore here, value could be just an id as well
                }
            }
            return null;
        }

        private MediaResource(string url)
            : this()
        {
            Uri uriResult;
            var valid = (Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                         (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp));
            if (!valid) throw new ClientError("invalid url for resource");
            Url = url;
        }

        private static ResourceDownloader GetDownloader()
        {
            return ResourceDownloader.Get();
        }

        /// <summary>
        /// Downloading resource and put in cache for later access no matter if this was already done before.
        /// </summary>
        public void Download()
        {
            if (CachingEnabled && GetDownloader() != null)
            {
                GetDownloader().Download(Url);
                IsCached = true;
            }
        }

        /// <summary>
        /// Return the contents of this resource as byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] GetContents()
        {
            var inStream = GetInputStream();
            if (inStream == null)
            {
                return null;
            }

            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                var bis = new BufferedStream(inStream);
                int read;
                while ((read = bis.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Loads this resource as a stream from its URL.
        ///  Note: If caching is enabled, (check {@link #isCachingEnabled()}, the resource content is also downloaded and cached
        ///  (if not already happened before, check {@link #isCached()}) and further invocations deliver from cache.
        /// </summary>
        /// <returns></returns>
        private Stream GetInputStream()
        {
            if (CachingEnabled && !IsCached)
            {
                // force download if not cached
                Download();
            }

            return GetDownloader() == null
                ? null
                : GetDownloader().GetInputStream(Url, CachingEnabled);
        }
    }
}