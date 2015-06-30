namespace Secucard.Connect.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Secucard.Model;

    public class RestRequest
    {
        public RestRequest()
        {
            Header = new NameValueCollection();
            BodyParameter = new Dictionary<string, string>();
        }

        public string Host { get; set; }
        public string PageUrl { get; set; }
        public string Token { get; set; }
        public string Method { get; set; }
        public NameValueCollection Header { get; set; }

        public string Id { get; set; }
        public QueryParams QueyParams { get; set; }

        public Dictionary<string, string> BodyParameter { get; set; }
        public SecuObject Object { get; set; }
        public string BodyJsonString { get; set; }
        public byte[] BodyBytes { get; set; }

        #region Handle Parameters

        // TODO: Move to other class
        public string GetPathAndQueryString()
        {
            if (string.IsNullOrWhiteSpace(Id))
                return string.Format("{0}{1}", PageUrl, EncodeQueryParams(QueryParamsToMap(QueyParams)));
            else
                return string.Format("{0}/{1}{2}", PageUrl, Id,
                    EncodeQueryParams(QueryParamsToMap(QueyParams)));
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
                foreach (var key in sortOrder.AllKeys)
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

                if (String.IsNullOrWhiteSpace(gq.Distance))
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

            //var array = (from key in queryParams.AllKeys
            //             from value in queryParams.GetValues(key)
            //             select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            var array = (from key in queryParams.AllKeys
                from value in queryParams.GetValues(key)
                select string.Format("{0}={1}", key, value)).ToArray();

            return "?" + string.Join("&", array);

        }

        #endregion



    }
}