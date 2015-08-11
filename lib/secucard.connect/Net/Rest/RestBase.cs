namespace Secucard.Connect.Net.Rest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Web;
    using Secucard.Connect.Net.Util;

    public class RestBase
    {
        //private const string UserAgent = "secucard client";
        private const string ContentTypeFrom = "application/x-www-form-urlencoded";
        private const string ContentTypeJson = "application/json";

        protected string BaseUrl { get; set; }


        #region  ### Ctor ###

        protected RestBase(string baseUrl)
        {
            BaseUrl = baseUrl;
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
        }

        #endregion

        #region ### Public ###

        public T RestPost<T>(RestRequest request)
        {
            var ret = RestPost(request);
            if (string.IsNullOrWhiteSpace(ret)) throw new Exception("no response"); // TODO: Create Exception

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public T RestPut<T>(RestRequest request)
        {
            var ret = RestPut(request);
            if (string.IsNullOrWhiteSpace(ret)) throw new Exception("no response"); // TODO: Create Exception

            return JsonSerializer.DeserializeJson<T>(ret);
        }


        public string RestPost(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Post;
            PrepareBody(request);
            var webRequest = FactoryWebRequest(request);

            webRequest.ContentLength = request.BodyBytes.Length;

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
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var wr = ex.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var restException = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = (ex.Status == WebExceptionStatus.ProtocolError) ? ((int?)wr.StatusCode) : null
                };
                reader.Close();
                throw restException;
            }
            return null;
        }

        public string RestPut(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Put;
            PrepareBody(request);

            var webRequest = FactoryWebRequest(request);
            webRequest.ContentType = ContentTypeJson;

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
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var wr = ex.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var restEx = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = (ex.Status == WebExceptionStatus.ProtocolError) ? ((int?)wr.StatusCode) : null
                };
                reader.Close();
                throw restEx;
            }

            return null;
        }

        protected string RestGet(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Get;
            var webRequest = FactoryWebRequest(request);
            webRequest.ContentType = ContentTypeJson;

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var wr = ex.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var restEx = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = (ex.Status == WebExceptionStatus.ProtocolError) ? ((int?)wr.StatusCode) : null
                };
                reader.Close();
                throw restEx;
            }

            return null;
        }

        protected string RestDelete(RestRequest request)
        {
            request.Method = "DELETE";

            var webRequest = FactoryWebRequest(request);
            webRequest.ContentType = ContentTypeJson;

            try
            {
                var webResponse = webRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();

                if (respStream != null)
                {
                    using (var reader = new StreamReader(respStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var wr = ex.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var restEx = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = (ex.Status == WebExceptionStatus.ProtocolError) ? ((int?)wr.StatusCode) : null
                };
                reader.Close();
                throw restEx;
            }

            return null;
        }

        protected string RestExecute(RestRequest request)
        {
            request.Method = WebRequestMethods.Http.Post;
            PrepareBody(request);
            var webRequest = FactoryWebRequest(request);

            webRequest.ContentLength = request.BodyBytes.Length;

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
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var wr = ex.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var restException = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = (ex.Status == WebExceptionStatus.ProtocolError) ? ((int?)wr.StatusCode) : null
                };
                reader.Close();
                throw restException;
            }
            return null;
        }

        private static void PrepareBody(RestRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.BodyJsonString))
            {
                request.BodyBytes = request.BodyJsonString.ToUTF8Bytes();
            }
            else
            {
                request.BodyBytes = BuildPostData(request.BodyParameter).ToUTF8Bytes();
            }
        }

        private HttpWebRequest FactoryWebRequest(RestRequest request)
        {
            var uri = string.Format("{0}{1}", BaseUrl, request.GetPathAndQueryString());

            var webRequest = (HttpWebRequest) WebRequest.Create(uri);

            webRequest.Method = request.Method;
            webRequest.Host = request.Host;
            webRequest.Accept = ContentTypeJson;
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            webRequest.Headers.Add("Accept-Charset", "utf-8");
            webRequest.UserAgent = request.UserAgent;

            if(string.IsNullOrWhiteSpace(request.BodyJsonString))
                webRequest.ContentType = ContentTypeFrom;
            else
                webRequest.ContentType = ContentTypeJson;

            if(request.Method==WebRequestMethods.Http.Put || request.Method==WebRequestMethods.Http.Post)
                webRequest.ContentLength = request.BodyBytes.Length;


            // Set authorization
            if (!string.IsNullOrWhiteSpace(request.Token))
                webRequest.Headers.Add("Authorization", string.Format("Bearer {0}", request.Token));

            webRequest.Headers.Add(request.Header);            // Other header info like user-agent

            return webRequest;
        }

        #endregion

        #region ### Private ###

        private static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }

        private static string BuildPostData(Dictionary<string, string> parameter)
        {
            var sb = new StringBuilder();
            foreach (var p in parameter)
            {
                if (sb.Length > 0) sb.Append("&");
                sb.AppendFormat("{0}={1}", p.Key, HttpUtility.UrlEncode(p.Value));
            }
            return sb.ToString();
        }

        #endregion
    }
}