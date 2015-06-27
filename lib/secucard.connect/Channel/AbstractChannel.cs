
using System;
using Secucard.Connect.Channel;
using Secucard.Model.Transport;

public abstract class AbstractChannel  {
  //protected PathResolver pathResolver = new PathResolver();

  //protected final Logger LOG = Logger.getLogger(getClass().getName());


  //protected void onFailed(Callback callback, Throwable e) {
  //  if (callback != null) {
  //    try {
  //      callback.failed(e);
  //    } catch (Exception e1) {
  //      // ignore
  //    }
  //  }
  //}

  //protected <T> void onCompleted(Callback<T> callback, T result) {
  //  if (callback != null) {
  //    try {
  //      callback.completed(result);
  //    } catch (Exception e) {
  //      // ignore
  //    }
  //  }
  //}

  //protected RuntimeException translateError(Status status, Throwable cause) {
  //  if (StringUtils.startsWithIgnoreCase(status.getError(), "product")) {
  //    return new ProductException(status, cause);
  //  }
  //  return new SecuException(status, cause);
  //}
}
