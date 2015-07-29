/**
 * Interface for secucard server communication via the standard secucard API, no matter what protocol is used.<br/>
 * See also <a href="http://secucard.com/developer/doc/index.html">here</a>.<br>
 * All methods should have a callback parameter supporting asyncronous invocation,
 * see {@link com.secucard.connect.Callback} for details.
 */

namespace Secucard.Connect.Channel
{
    using System;
    using System.Diagnostics.Tracing;
    using Secucard.Model;

    public interface IChannel
    {

   //     /**
   //* Open the channel and its resources.
   //*/
   //     void open(Func<object> callback);

   //     /**
   //* Registers a listener which gets called when a server side or other event happens.
   //* Server side events are not supported on any type of channels, e.g. REST based channels!
   //*/
        
   //     void setEventListener(EventListener listener);

   //     /**
   //* Retrieves a single object (resource) of a given type.<br/>
   //* Would invoke for example: GET /targetType/objectId .<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param targetType Type of the resource, will be used to determine target destination.
   //* @param objectId   The unique id of the object to retrieve.
   //* @param callback   The callback for async invocation.
   //* @return The object, never null. An exception is thrown if the id is unknown.
   //*/
   T GetObject<T>(string objectId) where T: SecuObject;

   //     /**
   //* Retrieves a collection of objects (resources) of a given type according to a given query.<br/>
   //* Would invoke for example: GET /targetType?queryParams.<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param targetType  Type of the resources to search for, will be used to determine actual target destination.
   //* @param queryParams The query parameter data. See {@link com.secucard.connect.model.QueryParams} for details.
   //* @param callback    The callback for async invocation.
   //* @return The object, null if nothing found.
   //*/
   ObjectList<T> FindObjects<T>(QueryParams queryParams) where T : SecuObject;

   //     /**
   //* Creating an object.<br/>
   //* Would invoke for example: POST /object with object mapped to JSON as request body.<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param object   The object holding data to create. The type/class of the object is used to determine the target
   //*                 destination.
   //* @param callback The callback for async invocation.
   //* @return The actual created object, never null. Throws exception if object cannot be created. May contain additional
   //* or corrected data, like id.  So using this object later on instead the provided is necessary.
   //*/
   //     T CreateObject<T>(Func<T> callback);

   //     /**
   //    * Updating an object.<br/>
   //    * Would invoke for example: PUT /object/objectId with object mapped to JSON as request body.<br/>
   //    * May throw an exception if an error happens.
   //    *
   //    * @param object   The object holding data to update with, must also provide unique source id. The type/class of the
   //    *                 object is used to determine the target destination.
   //    * @param callback The callback for async invocation.
   //    * @return The actual updated object, never null. Throws exception if object cannot be updated. May contain additional
   //    * or corrected data, like id.  So using this object later on instead the provided is necessary.
   //    */
   //     T UpdateObject<T>(Func<T> callback) where T : SecuObject;

   //     /**
   //    * Updating an object.<br/>
   //    * Would invoke for example: PUT /targetType/objectId/action/actionArg with arg mapped to JSON as request body.<br/>
   //    * May throw an exception if an error happens.
   //    *
   //    * @param targetType Type of the resource to update, will be used to determine actual target destination.
   //    * @param objectId   Id of the resource to update.
   //    * @param action     Additional action to execute.
   //    * @param actionArg  Additional argument to the action, optional.
   //    * @param arg        The new data to update with.
   //    * @param returnType Type of the returned result object, response will be mapped to this type.
   //    * @param callback   The callback for async invocation.
   //    * @return The actual updated object, or the result of the update, never null. Throws exception if object cannot be
   //    * updated. May contain additional or corrected data, like id. So using this object later on instead the provided is
   //    * necessary.
   //    */
   //     T UpdateObject<T>(string objectId, string action, string actionArg, object arg, T returnType, Func<T> callback);


   //     /**
   //* Deletes an object.<br/>
   //* Would invoke for example: DELETE /targetType/objectId.<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param targetType Type of the resource to delete, will be used to determine actual target destination.
   //* @param objectId   Id of the resource to delete.
   //* @param callback   The callback for async invocation.
   //*/
   //     void DeleteObject<T>(T targetType, string objectId, Func<object> callback);


   //     /**
   //* Deletes an object.<br/>
   //* Would invoke for example: DELETE /targetType/objectId/action/actionArg.<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param targetType Type of the resource to delete, will be used to determine actual target destination.
   //* @param objectId   Id of the resource to delete.
   //* @param action     Additional action to execute.
   //* @param actionArg  Additional argument to the action, optional.
   //* @param callback   The callback for async invocation.
   //*/
   //     void DeleteObject<T>(T targetType, string objectId, string action, string actionArg, Func<object> callback);

   //     /**
   //* Execute an action.<br/>
   //* Would invoke for example: POST /targetType/objectId/action/actionArg with arg as JSON request body.<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param targetType Type of the resource, will be used to determine target destination.
   //* @param objectId   Id of a resource.
   //* @param action     Action to execute.
   //* @param actionArg  Additional argument to the action, optional.
   //* @param arg        The data to be processed by the action.
   //* @param returnType Type of the returned result object, response will be mapped to this type.
   //* @param callback   The callback for async invocation.
   //* @return The result of the execution, never null. An exception is thrown if the action cannot be executed.
   //*/
   //     T Execute<T>(string objectId, string action, string actionArg, object arg, T returnType, Func<T> callback);

   //     /**
   //* Execute an custom action.<br/>
   //* Would invoke for example: POST /General/Apps/appId/callBackend/action with arg as JSON request body.<br/>
   //* May throw an exception if an error happens.
   //*
   //* @param appId      Id of a application for which the given action is executed.
   //* @param action     Action to execute.
   //* @param arg        The data to be processed by the action.
   //* @param returnType Type of the returned result object, response will be mapped to this type.
   //* @param callback   The callback for async invocation.
   //* @return The result of the execution, never null. An exception is thrown if the action cannot be executed.
   //*/
   //     T Execute<T>(string appId, string action, object arg, T returnType, Func<T> callback);

   //     /**
   //* Close channel and release resources.
   //*/
   //     void Close(Func<object> callback);

    }
}
