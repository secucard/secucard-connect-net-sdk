namespace secucard.connect.rest
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
    using secucard.connect.Rest;
    using secucard.model;

    public class RestBase
    {
        private const string UserAgent = "secucard client";
        private const string ContentTypeFrom = "application/x-www-form-urlencoded";
        private const string ContentTypeJson = "application/json";

        #region  ### Ctor ###

        public RestBase()
        {
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
        }

        #endregion

        #region ### Public ###

        public T RestPost<T>(RestRequest request)
        {
            var ret = RestPost(request);
            if (string.IsNullOrWhiteSpace(ret)) throw new Exception("no response"); // TODO: Create Execption

            try
            {
                var x = JsonSerializer.DeserializeJson<T>(ret);
                return x;
            }
            catch (Exception)
            {
                // TODO: Create Execption
                throw;
            }
        }

        public string RestPost(RestRequest request)
        {
            var webRequest =
                (HttpWebRequest)
                    WebRequest.Create(string.Format("{0}{1}", request.BaseUrl, request.PageUrl.TrimStart('/')));
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.UserAgent = UserAgent;
            webRequest.Host = request.Host;
            webRequest.ContentType = ContentTypeFrom;
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            var postBytes = BuildPostData(request.Parameter).ToUTF8Bytes();
            webRequest.ContentLength = postBytes.Length;

            var reqStream = webRequest.GetRequestStream();
            reqStream.Write(postBytes, 0, postBytes.Length);

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
            catch (WebException oEx)
            {
                var wr = oEx.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var ex = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = wr.GetResponseHeader("Status")
                };
                reader.Close();
                throw ex;
            }
            return null;
        }

        public string RestPut(RestRequest request)
        {
            var webRequest =
                (HttpWebRequest)
                    WebRequest.Create(string.Format("{0}{1}", request.BaseUrl, request.PageUrl.TrimStart('/')));
            webRequest.Method = WebRequestMethods.Http.Put;
            webRequest.UserAgent = UserAgent;
            webRequest.Host = request.Host;
            webRequest.Accept = ContentTypeJson;
            webRequest.ContentType = ContentTypeJson;
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            webRequest.Headers.Add("Accept-Charset", "utf-8");

            var bodyBytes = request.BodyJsonString.ToUTF8Bytes();
            webRequest.ContentLength = bodyBytes.Length;

            var reqStream = webRequest.GetRequestStream();
            reqStream.Write(bodyBytes, 0, bodyBytes.Length);

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
            catch (WebException oEx)
            {
                var wr = oEx.Response as HttpWebResponse;
                var dataStream = wr.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.UTF8);
                var ex = new RestException
                {
                    BodyText = reader.ReadToEnd(),
                    StatusDescription = wr.StatusDescription,
                    StatusCode = wr.GetResponseHeader("Status")
                };
                reader.Close();
                throw ex;
            }

            return null;
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