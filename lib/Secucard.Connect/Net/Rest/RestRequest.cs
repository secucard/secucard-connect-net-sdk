namespace Secucard.Connect.Net.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Secucard.Connect.Product.Common.Model;

    public class RestRequest
    {
        public RestRequest()
        {
            Header = new NameValueCollection();
            BodyParameter = new Dictionary<string, string>();
            ActionParameter = new List<string>();
        }

        public string Url { get; set; }
        public string Host { get; set; }
        public string PageUrl { get; set; }
        public string Action { get; set; }
        public List<string> ActionParameter { get; set; }
        public string Token { get; set; }
        public string Method { get; set; }
        public NameValueCollection Header { get; set; }
        public string UserAgent { get; set; }

        public string Id { get; set; }
        public QueryParams QueryParams { get; set; }

        public Dictionary<string, string> BodyParameter { get; set; }
        public object Object { get; set; }
        public Type ObjetType { get; set; }
        public List<SecuObject> Objects { get; set; }
        public string BodyJsonString { get; set; }
        public byte[] BodyBytes { get; set; }


        public void PrepareBody()
        {
            BodyBytes = !string.IsNullOrWhiteSpace(BodyJsonString)
                ? BodyJsonString.ToUTF8Bytes()
                : BuildPostData(BodyParameter).ToUTF8Bytes();
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

        #region Handle Parameters

        // TODO: Move to other class
        public string GetPathAndQueryString()
        {
            string s = PageUrl;
            // Include id in path
            if (!string.IsNullOrWhiteSpace(Id)) s += "/" + Id;
            // Include action in path
            if (!string.IsNullOrWhiteSpace(Action)) s += "/" + Action;
            // Include actionParmater in path
            if (ActionParameter != null && ActionParameter.Any()) s += "/" + string.Join("/", ActionParameter);
            // add query parameters at end
            return string.Format("{0}{1}", s, EncodeQueryParams(QueryParamsToMap(QueryParams)));
        }

        private static NameValueCollection QueryParamsToMap(QueryParams queryParams)
        {
            var nvc = new NameValueCollection();

            if (queryParams == null)
            {
                return nvc;
            }

            if (queryParams.Expand.HasValue && queryParams.Expand.Value) nvc.Add("expand", "true");

            bool scroll = !string.IsNullOrWhiteSpace(queryParams.ScrollId);
            bool scrollExpire = !string.IsNullOrWhiteSpace(queryParams.ScrollExpire);

            if (scroll)
            {
                nvc.Add("scroll_id", queryParams.ScrollId);
            }

            if (scrollExpire)
            {
                nvc.Add("scroll_expire", queryParams.ScrollExpire);
            }

            if (!scroll && queryParams.Count != null && queryParams.Count >= 0)
            {
                nvc.Add("count", queryParams.Count.ToString());
            }

            if (!scroll && !scrollExpire && queryParams.Offset.HasValue && queryParams.Offset > 0)
            {
                nvc.Add("offset", queryParams.Offset.ToString());
            }

            var fields = queryParams.Fields;
            if (!scroll && fields != null && fields.Count > 0)
            {
                // add "," separated list of field names
                string names = null;
                foreach (var field in fields)
                {
                    names = names == null ? "" : names + ',';
                    names += field;
                }
                nvc.Add("fields", names);
            }

            var sortOrder = queryParams.SortOrder;
            if (!scroll && sortOrder != null)
            {
                foreach (var key in sortOrder.Keys)
                {
                    nvc.Add("sort[" + key + "]", sortOrder[key]);
                }
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Query))
            {
                nvc.Add("q", queryParams.Query);
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Preset))
            {
                nvc.Add("preset", queryParams.Preset);
            }

            var gq = queryParams.GeoQueryObj;
            if (gq != null)
            {
                if (!string.IsNullOrWhiteSpace(gq.Field))
                {
                    nvc.Add("geo[field]", gq.Field);
                }

                if (gq.Lat != null)
                {
                    nvc.Add("geo[lat]", gq.Lat.ToString());
                }

                if (gq.Lon != null)
                {
                    nvc.Add("geo[lon]", gq.Lon.ToString());
                }

                if (string.IsNullOrWhiteSpace(gq.Distance))
                {
                    nvc.Add("geo[distance]", gq.Distance);
                }
            }

            return nvc;
        }

        private static string EncodeQueryParams(NameValueCollection queryParams)
        {
            if (queryParams == null || !queryParams.HasKeys())
            {
                return null;
            }

            var array = (from key in queryParams.AllKeys
                from value in queryParams.GetValues(key)
                select string.Format("{0}={1}", key, value)).ToArray();

            return "?" + string.Join("&", array);
        }

        #endregion
    }
}