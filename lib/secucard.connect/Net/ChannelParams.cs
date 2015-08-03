namespace Secucard.Connect.Channel
{
    using System;
    using Secucard.Connect.Net;
    using Secucard.Model;

    public class ChannelParams {
    public string[] Objects; // target object type, consisting of 2 parts
    public string ObjectId; // id of resource
    public string AppId;
    public string Action;
    public string ActionArg;
    public object Data; // payload
    public Type ReturnType; // object type of response
    public QueryParams QueryParams;
    public ChannelOptions Options;

  
    // some typical used constructors ----------------------------------------------------------------------------------


    //public Params(string[] obj, string objectId, string appId, string action, String actionArg, object data,
    //              Class returnType, QueryParams queryParams, ApiOptions options) {
    //  this.objects = obj;
    //  this.objectId = objectId;
    //  this.appId = appId;
    //  this.action = action;
    //  this.actionArg = actionArg;
    //  this.data = data;
    //  this.returnType = returnType;
    //  this.queryParams = queryParams;
    //  this.options = options;
    //}

    //public Params(string[] object, string objectId, Options options) {
    //  this(object, objectId, null, null, null, null, null, null, options);
    //}

    //public Params(string[] object, string objectId, Class returnType, Options options) {
    //  this(object, objectId, null, null, null, null, returnType, null, options);
    //}

    //public Params(string[] object, QueryParams queryParams, Class returnType, Options options) {
    //  this(object, null, null, null, null, null, returnType, queryParams, options);
    //}

    //public Params(string[] object, object data, Class returnType, Options options) {
    //  this(object, null, null, null, null, data, returnType, null, options);
    //}

    //public Params(string[] object, string objectId, Object data, Class returnType, Options options) {
    //  this(object, objectId, null, null, null, data, returnType, null, options);
    //}

    //public Params(string[] object, string objectId, String action, string actionArg, Object data,
    //              Class returnType, Options options) {
    //  this(object, objectId, null, action, actionArg, data, returnType, null, options);
    //}

    //public Params(string[] object, string objectId, String action, string actionArg, Options options) {
    //  this(object, objectId, null, action, actionArg, null, null, null, options);
    //}

    //public static Params forApp(string appId, string action, object payload, Class returnType, Options options) {
    //  return new Params(null, null, appId, action, null, payload, returnType, null, options);
    //}
  }


  /**
   * Joins the target parts with the separator and capitalizes.
   */
  //protected static string buildTarget(string[] parts, char separator) {
  //  if (parts == null || parts.length < 2) {
  //    throw new IllegalArgumentException("Invalid target specification.");
  //  }
  //  return parts[0] + separator + parts[1];
  //}

}
