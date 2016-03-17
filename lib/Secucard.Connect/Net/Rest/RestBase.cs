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

namespace Secucard.Connect.Net.Rest
{
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Util;

    public class RestBase
    {
        private const string ContentTypeFrom = "application/x-www-form-urlencoded";
        private const string ContentTypeJson = "application/json";

        #region  ### Ctor ###

        protected RestBase(RestConfig restConfig)
        {
            RestConfig = restConfig;
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            SecucardTrace.Info(RestConfig.Url);
        }

        #endregion

        private RestConfig RestConfig { get; set; }

        #region ### Private Static ###

        private static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #endregion

        #region ### Methods ###

        public string RestPost(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Post;
            request.PrepareBody();
            var webRequest = FactoryWebRequest(request);

            var reqStream = webRequest.GetRequestStream();
            reqStream.Write(request.BodyBytes, 0, request.BodyBytes.Length);

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        var ret = reader.ReadToEnd();
                        SecucardTrace.Info("response:\n{0}", ret);
                        return ret;
                    }
                }
            }
            catch (WebException ex)
            {
                var restException = HandelWebException(ex);
                throw restException;
            }
            return null;
        }

        public string RestPut(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Put;
            request.PrepareBody();

            var webRequest = FactoryWebRequest(request);

            var reqStream = webRequest.GetRequestStream();
            reqStream.Write(request.BodyBytes, 0, request.BodyBytes.Length);

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        var ret = reader.ReadToEnd();
                        SecucardTrace.Info("response:\n{0}", ret);
                        return ret;
                    }
                }
            }
            catch (WebException ex)
            {
                var restException = HandelWebException(ex);
                throw restException;
            }

            return null;
        }

        protected string RestGet(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Get;
            var webRequest = FactoryWebRequest(request);
            RestTrace(webRequest, request.BodyBytes);

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        var ret = reader.ReadToEnd();
                        SecucardTrace.Info("response:\n{0}", ret);
                        return ret;
                    }
                }
            }
            catch (WebException ex)
            {
                var restException = HandelWebException(ex);
                throw restException;
            }

            return null;
        }

        protected string RestDelete(RestRequest request)
        {
            request.Method = "DELETE";

            var webRequest = FactoryWebRequest(request);

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        var ret = reader.ReadToEnd();
                        SecucardTrace.Info("response:\n{0}", ret);
                        return ret;
                    }
                }
            }
            catch (WebException ex)
            {
                var restException = HandelWebException(ex);
                throw restException;
            }

            return null;
        }

        protected string RestExecute(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Post;
            request.PrepareBody();
            var webRequest = FactoryWebRequest(request);

            var reqStream = webRequest.GetRequestStream();
            reqStream.Write(request.BodyBytes, 0, request.BodyBytes.Length);

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        var ret = reader.ReadToEnd();
                        SecucardTrace.Info("response:\n{0}", ret);
                        return ret;
                    }
                }
            }
            catch (WebException ex)
            {
                var restException = HandelWebException(ex);
                throw restException;
            }
            return null;
        }

        protected Stream RestGetStream(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Get;
            var webRequest = (HttpWebRequest) WebRequest.Create(request.Url);
            webRequest.UserAgent = request.UserAgent;

            // Set authorization
            if (!string.IsNullOrWhiteSpace(request.Token))
                webRequest.Headers.Add("Authorization", string.Format("Bearer {0}", request.Token));

            webRequest.Headers.Add(request.Header); // Other header info like user-agent
            RestTrace(webRequest, request.BodyBytes);

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();
                SecucardTrace.Info("Response stream arrived.");
                return respStream;
            }
            catch (WebException ex)
            {
                var restException = HandelWebException(ex);
                throw restException;
            }
        }

        #endregion

        #region ### Private Methods ###

        private static RestException HandelWebException(WebException ex)
        {
            SecucardTrace.Exception(ex);
            var wr = ex.Response as HttpWebResponse;
            var dataStream = wr?.GetResponseStream();
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var restException = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = (ex.Status == WebExceptionStatus.ProtocolError) ? ((int?) wr.StatusCode) : null
                };
                SecucardTrace.Exception(restException);
                SecucardTrace.Info(restException.StatusDescription);
                SecucardTrace.Info(restException.BodyText.EscapeCurlyBracets());
                reader.Close();
                return restException;
            }

            throw ex;
        }

        private HttpWebRequest FactoryWebRequest(RestRequest request)
        {
            var uri = string.Format("{0}{1}", RestConfig.Url, request.GetPathAndQueryString());

            var webRequest = (HttpWebRequest) WebRequest.Create(uri);

            webRequest.Method = request.Method;
            webRequest.Timeout = RestConfig.ConnectTimeoutSec * 1000;
            webRequest.Host = request.Host;
            webRequest.Accept = ContentTypeJson;
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            webRequest.Headers.Add("Accept-Charset", "utf-8");
            webRequest.UserAgent = request.UserAgent;

            if (string.IsNullOrWhiteSpace(request.BodyJsonString))
                webRequest.ContentType = ContentTypeFrom;
            else
                webRequest.ContentType = ContentTypeJson;

            if (request.Method == WebRequestMethods.Http.Put || request.Method == WebRequestMethods.Http.Post)
                webRequest.ContentLength = request.BodyBytes.Length;


            // Set authorization
            if (!string.IsNullOrWhiteSpace(request.Token))
                webRequest.Headers.Add("Authorization", string.Format("Bearer {0}", request.Token));

            webRequest.Headers.Add(request.Header); // Other header info like user-agent

            RestTrace(webRequest, request.BodyBytes);

            return webRequest;
        }

        private static void RestTrace(HttpWebRequest webRequest, byte[] body)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("request:\n");
            sb.AppendLine();
            sb.AppendFormat("{0}:{1}", webRequest.Method, webRequest.RequestUri.AbsoluteUri);
            sb.AppendLine();
            sb.AppendFormat("Host: {0}", webRequest.Host);
            sb.AppendLine();
            sb.AppendFormat("Type: {0}, Length: {1}", webRequest.ContentType, webRequest.ContentLength);
            sb.AppendLine();
            if (body != null)
            {
                sb.AppendFormat("Body: {0}", Encoding.UTF8.GetString(body));
                sb.AppendLine();
            }

            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = string.Empty;
            if (type != null) name = type.Name;

            var source = string.Format("{0}.{1}", name, method.Name);

            SecucardTrace.InfoSource(source, sb.ToString().Replace("{", "{{").Replace("}", "}}"));
        }

        #endregion
    }
}