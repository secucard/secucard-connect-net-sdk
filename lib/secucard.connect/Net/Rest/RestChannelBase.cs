using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using Secucard.Connect.Auth;
using Secucard.Connect.Channel;
using Secucard.Connect.Rest;
using Secucard.Model;

public abstract class RestChannelBase : Channel {

  protected RestConfig configuration;
  public bool secure = true;
  protected string id;
  public AuthProvider AuthProvider;

  public RestChannelBase(RestConfig configuration, string id) {
    this.configuration = configuration;
    this.id = id;
  }

  //@Override
  //public void setEventListener(EventListener listener) {
  //  throw new UnsupportedOperationException("Rest channel doesn't support listener.");
  //}

  

  protected Dictionary<string, object> queryParamsToMap(QueryParams queryParams) {
    Dictionary<string, object> map = new Dictionary<string, object>();

    if (queryParams == null) {
      return map;
    }

    bool scroll = !string.IsNullOrWhiteSpace(queryParams.ScrollId);
    bool scrollExpire = !string.IsNullOrWhiteSpace(queryParams.ScrollExpire);

    if (scroll) {
      map.Add("scroll_id", queryParams.ScrollId);
    }

    if (scrollExpire) {
      map.Add("scroll_expire", queryParams.ScrollExpire);
    }

    if (!scroll && queryParams.Count != null && queryParams.Count >= 0) {
      map.Add("count", queryParams.Count.ToString());
    }

    if (!scroll && !scrollExpire && queryParams.Offset.HasValue && queryParams.Offset > 0) {
      map.Add("offset", queryParams.Offset.ToString());
    }

    List<string> fields = queryParams.Fields;
    if (!scroll && fields != null && fields.Count  > 0) {
      // add "," separated list of field names
      string names = null;
      foreach (string field in fields) {
        names = names == null ? "" : names + ',';
        names += field;
      }
      map.Add("fields", names);
    }

    var sortOrder = queryParams.SortOrder;
    if (!scroll && sortOrder != null) {
       foreach ( var key in sortOrder.AllKeys) {
        map.Add("sort[" + key + "]", sortOrder[key]);
      }
    }

    if (!string.IsNullOrWhiteSpace(queryParams.Query)) {
      map.Add("q", queryParams.Query);
    }

    if (!string.IsNullOrWhiteSpace(queryParams.Preset)) {
      map.Add("preset", queryParams.Preset);
    }

    QueryParams.GeoQuery gq = queryParams.GeoQueryObj;
    if (gq != null) {
      if (!string.IsNullOrWhiteSpace(gq.Field)) {
        map.Add("geo[field]", gq.Field);
      }

      if (gq.Lat != null) {
        map.Add("geo[lat]", gq.Lat.ToString());
      }

      if (gq.Lon != null) {
        map.Add("geo[lon]", gq.Lon.ToString());
      }

      if (String.IsNullOrWhiteSpace(gq.Distance)) {
        map.Add("geo[distance]", gq.Distance);
      }
    }

    return map;
  }

  //protected string encodeQueryParams(QueryParams queryParams) {
  //  return encodeQueryParams(queryParamsToMap(queryParams));
  //}

  //protected string encodeQueryParams(Dictionary<string, object> queryParams) {
  //  if (queryParams == null || !queryParams.Any()) {
  //    return null;
  //  }
  //  var encodedParams = new StringBuilder();
  //  string paramsEncoding = "UTF-8";
  //  //try {
  //  //  for (Map.Entry<string, object> entry : queryParams.entrySet()) {
  //  //    encodedParams.append(URLEncoder.encode(entry.getKey(), paramsEncoding));
  //  //    encodedParams.append('=');
  //  //    encodedParams.append(URLEncoder.encode((string) entry.getValue(), paramsEncoding));
  //  //    encodedParams.append('&');
  //  //  }
  //  //  return encodedParams.toString();
  //  //} catch (UnsupportedEncodingException uee) {
  //  //  throw new RuntimeException("Encoding not supported: " + paramsEncoding, uee);
  //  //}
  //    return null;
  //}

  protected void SetAuthorizationHeader(Dictionary<string,string> headers, string token) {
    if (token != null) {
      string key = "Authorization";
      string value = "Bearer " + token;
      //if (headers instanceof MultivaluedMap) {
      //  ((MultivaluedMap) headers).putSingle(key, value);
      //} else {
        headers.Add(key, value);
      //}
    }
  }

  /**
   * Low level rest access for internal usage, posting to a url and get the response back as object.
   */
  //public abstract <T> T post(String url, Map<String, Object> parameters, Map<String, String> headers, Class<T> responseType,
  //                           Integer... ignoredState);

  //public abstract InputStream getStream(String url, Map<String, Object> parameters, Map<String, String> headers,
  //                                      Callback<InputStream> callback);
}
