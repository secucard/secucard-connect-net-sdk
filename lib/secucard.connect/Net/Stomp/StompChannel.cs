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

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Secucard.Connect.Net.Util;
using Secucard.Stomp;

namespace Secucard.Connect.Net.Stomp
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Rest;

    public class StompChannel : Channel
    {
        //protected static final String HEADER_CORRELATION_ID = "correlation-id";
        //protected static final String STATUS_OK = "ok";

        private readonly ConcurrentDictionary<string, StompMessage> Messages = new ConcurrentDictionary<string, StompMessage>();
        private readonly StompConfig Configuration;
        private StompClient Stomp;
        private string ConnectToken;
        private volatile bool IsConfirmed;
        private volatile bool StopRefresh;
        private Thread refreshThread;
        private string Id;

        //private final StatusHandler defaultStatusHandler = new StatusHandler() {
        //  @Override
        //  public boolean hasError(Message message) {
        //    return !message.getStatus().equalsIgnoreCase(STATUS_OK);
        //  }
        //};

        public StompChannel(StompConfig configuration, ClientContext context)
            : base(context)
        {
            Configuration = configuration;

            Id = Guid.NewGuid().ToString();
            Stomp = new StompClient(Configuration);
        }

        private void Trace(string fmt, params object[] args)
        {
            Context.SecucardTrace.Info(fmt,args);
        }

        /**
     * Must not be synchronized, because of session refresh in separate thread.
     * Just used one time when client is opened.
     */
        public override void Open()
        {
            Connect(GetToken());
        }

        public override void Close()
        {
            StopRefresh = true;
            Stomp.Disconnect();
            Trace("STOMP channel closed.");
        }

        public override T Request<T>(ChannelRequest request)
        {
            StompDestination dest = CreateDestination(request);
            // StompMessage message = new StompMessage(request.ObjectId, request.ActionArgs, request.QueryParams, request.Object);

            return SendMessage<T>(dest, request, request.ChannelOptions.TimeOutSec);
        }


        public override ObjectList<T> RequestList<T>(ChannelRequest request)
        {
            StompDestination dest = CreateDestination(request);
            return SendMessage<ObjectList<T>>(dest, request, request.ChannelOptions.TimeOutSec);

            //StompMessage message = new StompMessage<>(params.objectId, params.actionArg, params.queryParams, params.data);

            //StatusHandler statusHandler = new StatusHandler() {
            //  @Override
            //  public boolean hasError(Message message) {
            //    // treat not found as ok here, no matches for query
            //    return !(STATUS_OK.equalsIgnoreCase(message.getStatus())
            //        || "ProductNotFoundException".equalsIgnoreCase(message.getError()));
            //  }
            //};

            //return sendMessage(dest, message, new MessageListTypeRef(params.returnType), statusHandler, callback,
            //    params.options.timeOutSec);
            return null;
        }


        private static StompDestination CreateDestination(ChannelRequest request)
        {
            StompDestination dest;

            dest = new StompDestination(request.Product, request.Resource);
            dest.Obj = request.Object;

            //if (request.Object != null) {
            //  dest = new StompDestination(request.Product, request.Resource);
            //  dest.Obj = request.Object;
            //} else if (request.AppId != null) {
            //  //dest = new AppDestination(params.appId);
            //} else {
            //  //throw new IllegalArgumentException("Missing object spec or app id.");
            //}

            switch (request.Method)
            {
                case ChannelMethod.GET:
                    dest.Command = "get:";
                    break;
                case ChannelMethod.CREATE:
                    dest.Command = "add:";
                    break;
                case ChannelMethod.EXECUTE:
                    dest.Command = "exec:";
                    break;
                case ChannelMethod.UPDATE:
                    dest.Command = "update:";
                    break;
                case ChannelMethod.DELETE:
                    dest.Command = "delete:";
                    break;
                default:
                    // throw new IllegalArgumentException("Invalid method arg");
                    break;
            }

            dest.Action = request.Action;
            return dest;
        }



        
        /// <summary>
        /// Provides the token used as login and password for STOMP connect.
        /// </summary>
        private string GetToken()
        {
            return Context.TokenManager.GetToken(false);
        }


        /**
     * Connect to STOMP Server.
     * If the connection fails all resources are closed.
     *
     * @param token The token used as login/password. May be null.
     * @throws IllegalStateException If no connect credentials available.
     * @throws ClientError           If any  error happens.
     */
        private void Connect(string token)
        {
            if (token == null) token = Configuration.Login;

            Stomp.Config.Login = token;
            Stomp.Config.Password = token;
            Stomp.Connect();
            ConnectToken = token;

            //if (eventListener != null) {
            //  eventListener.onEvent(StompEvents.STOMP_CONNECTED);
            //}
        }

        private T SendMessage<T>(StompDestination destination, ChannelRequest request, int? timeoutSec) 
        {

            return DoSendMessage<T>(destination, request.Object, timeoutSec, false);
            //return new Execution<T>() {
            //  @Override
            //  protected T execute() {
            //    return DoSendMessage(destinationSpec, arg, returnType, statusHandler, timeout);
            //  }
            //}.start(callback);
        }

        private T DoSendMessage<T>(StompDestination destination, object obj, int? timeoutSec, bool isConfirmed) 
        {
            IsConfirmed = isConfirmed;
            string token = GetToken();

            // auto-connect or reconnect if token has changed since last connect
            if (Stomp.StompClientStatus != EnumStompCoreStatus.Connected || (token != null && !token.Equals(ConnectToken)))
            {
                if (Stomp.StompClientStatus == EnumStompCoreStatus.Connected)
                {
                    Trace("Reconnect due token change.");
                }
                try
                {
                    Stomp.Disconnect();
                }
                catch (Exception e)
                {
                    // just log...
                    Trace("Error disconnecting. {0}", e);
                }
                Connect(token);
            }

            StompFrame frame = new StompFrame(destination.Command);
            frame.Headers.Add(StompHeader.ReplyTo, Configuration.ReplyQueue);
            frame.Headers.Add(StompHeader.ContentType, "application/json");
            frame.Headers.Add(StompHeader.UserId, token);
            frame.Headers.Add(StompHeader.CorrelationId, CreateCorrelationId());
            frame.Headers.Add(StompHeader.Destination, destination.ToString());

            //if (destinationSpec instanceof AppDestination) {
            //  header.put("app-id", ((AppDestination) destinationSpec).appId);
            //}

            try
            {
                frame.Body = JsonSerializer.SerializeJson(obj);
                frame.Headers.Add("content-length", frame.Body.ToUTF8Bytes().ToString());
            }
            catch (Exception e)
            {
                Trace("error {0}", e);
            }

            //    Stomp.SendFrame(destination.ToString(), body, header, timeoutSec);
            Stomp.SendFrame(frame);

            //string answer = awaitAnswer(corrId, timeoutSec);
            //Message<T> msg;
            //try {
            //  msg = context.jsonMapper.map(answer, returnType);
            //} catch (Exception e) {
            //  throw new ClientError("Error unmarshalling message.", e);
            //}

            //if (msg == null) {
            //  return null;
            //}

            //statusHandler.check(msg);

            //isConfirmed = true;

            //return msg.getData();

            return (T) new object() ;
        }

        ///**
        // * Starts the session refresh loop thread. Blocks until the loop is really running and returns after that.
        // *
        // * @return Null if successfully started or an error if not.
        // */
        //public void StartSessionRefresh() {
        //  // first stop if running and wait until finished.
        //  StopRefresh = true;
        //  if (refreshThread != null && refreshThread.isAlive()) {
        //    Trace("Refresh thread still running, wait for completion.");
        //    try {
        //      refreshThread.join();
        //    } catch (InterruptedException e) {
        //      // ignore
        //    }
        //  }

        //  final AtomicReference<Throwable> reference = new AtomicReference<>(null);

        //  final CountDownLatch latch = new CountDownLatch(1);
        //  stopRefresh = false;
        //  refreshThread = new Thread() {
        //    @Override
        //    public void run() {
        //      reference.set(runSessionRefresh(latch));
        //      latch.countDown();
        //    }
        //  };

        //  refreshThread.setDaemon(true);
        //  refreshThread.start();

        //  // let current thread
        //  try {
        //    latch.await();
        //  } catch (InterruptedException e) {
        //    // ignore
        //  }

        //  // must return null if no error happened
        //  return reference.get();
        //}

        ///**
        // * Sends a confirmations within an fixed interval that this client is alive.
        // * But is able to skip confirmation if other already confirmed this (setting isConfirmed to true).
        // * Always keeps trying to refresh next time even if an attempt failed.
        // *
        // * @param countDownLatch A latch to release if successfully executed.
        // */
        //private Throwable runSessionRefresh(CountDownLatch countDownLatch) {
        //  LOG.info("Session refresh loop started.");
        //  boolean initial = true;
        //  do {
        //    try {
        //      LOG.debug("Try session refresh.");
        //      Options options = Options.getDefault();
        //      options.timeOutSec = 5; // should timeout sooner as by config to detect connection failure
        //      Request(ChannelMethod.EXECUTE, new Params(new string[]{"auth", "sessions"}, "me", "refresh", null, null, Result.class,
        //          options), null);
        //      isConfirmed = false;
        //      LOG.info("Session refresh sent.");
        //    } catch (Throwable t) {
        //      LOG.info("Session refresh failed.");
        //      if (initial) {
        //        // first invocation after connect, let client know something is going wrong
        //        return t;
        //      }
        //      // try next time
        //    }

        //    // releases latch
        //    if (initial) {
        //      countDownLatch.countDown();
        //      initial = false;
        //    }

        //    // sleep until next refresh, reset wait time if anybody confirmed session for us so we can sleep longer
        //    new ThreadSleep() {
        //      @Override
        //      protected boolean reset() {
        //        if (isConfirmed) {
        //          isConfirmed = false;
        //          return true;
        //        }
        //        return false;
        //      }

        //      @Override
        //      protected boolean cancel() {
        //        return stopRefresh;
        //      }
        //    }.execute(configuration.heartbeatSec * 1000, 500, TimeUnit.MILLISECONDS);

        //  } while (!stopRefresh);

        //  LOG.info("Session refresh stopped.");

        //  return null;
        //}

        /// <summary>
        /// Put message in message list
        /// </summary>
        private void PutMessage(string id, string body)
        {
            StompMessage msg = new StompMessage(id, body);
            bool success = Messages.TryAdd(msg.Id, msg);
            if (!success) throw new Exception("Invalid correlation id, message with this id already exists.");

        }

        /// <summary>
        /// Pull Message from Queue an remove old messages if exists.
        /// </summary>
        private string PullMessage(string id, int maxMessageAgeSec)
        {
            DateTime t = System.DateTime.Now.AddSeconds(-maxMessageAgeSec);
            StompMessage message;

            var toOld = Messages.Where(o => o.Value.ReceiveTime < t).ToList();
            foreach (var m in toOld)
            {
                Messages.TryRemove(m.Key, out message);
            }

            if (Messages.TryRemove(id, out message))
            {
                return message.Body ?? "";
            }
            else
            {
                return null;
            }


        }

        private string CreateCorrelationId()
        {
            return Id + "-" + Guid.NewGuid() + "-" + DateTime.Now.Ticks;
        }

        //private string awaitAnswer(final String id, Integer timeoutSec) {
        //  if (timeoutSec == null) {
        //    timeoutSec = configuration.messageTimeoutSec;
        //  }
        //  long maxWaitTime = System.currentTimeMillis() + timeoutSec * 1000;
        //  string msg = null;
        //  while (System.currentTimeMillis() <= maxWaitTime) {
        //    synchronized (messages) {
        //      if (messages.containsKey(id)) {
        //        msg = pullMessage(id, messages, configuration.maxMessageAgeSec);
        //        break;
        //      }
        //    }
        //    try {
        //      Thread.sleep(100);
        //    } catch (InterruptedException e) {
        //      // will be stopped anyway
        //    }
        //  }

        //  if (msg == null) {
        //    throw new MessageTimeoutException("No answer for " + id + " received within " + timeoutSec + "s.");
        //  }

        //  return msg;
        //}


        // Inner Classes -----------------------------------------------------------------------------------------------------


        ///**
        // * The default {@code Message<T>} type reference object.
        // */
        //protected static class MessageTypeRef extends DynamicTypeReference<Void> {
        //  public MessageTypeRef(Class type) {
        //    super(Message.class, type);
        //  }
        //}

        ///**
        // * The {@code Message<ObjectList<T>>} type reference object.
        // */
        //protected static class MessageListTypeRef extends DynamicTypeReference<Void> {
        //  public MessageListTypeRef(Class type) {
        //    super(Message.class, new TypeInfo(ObjectList.class, type));
        //  }
        //}




        //protected abstract class StatusHandler {
        //  public abstract boolean hasError(Message message);

        //  public void check(Message message) {
        //    if (hasError(message)) {
        //      throw new ServerErrorException(message);
        //    }
        //  }
        //}

        // Default Stomp Message Handling ------------------------------------------------------------------------------------

        //private class DefaultEventListner implements StompClient.Listener {

        //  @Override
        //  public void onMessage(Frame frame) {
        //    string correlationId = frame.getHeaders().get(HEADER_CORRELATION_ID);
        //    string body = frame.getBody();

        //    if (body == null) {
        //      return;
        //    }

        //    if (correlationId != null) {
        //      synchronized (messages) {
        //        putMessage(correlationId, body, messages);
        //      }
        //    } else if (eventListener != null) {
        //      // this is an STOMP event message, no direct correlation to a request
        //      LOG.debug("STOMP event message received: ", body);

        //      // todo: event type testing

        //      Object event = null;
        //      try {
        //        // we expect Event type at first
        //        event = context.jsonMapper.map(body, Event.class);
        //      } catch (Exception e) {
        //        // ignore
        //      }

        //      if (event != null) {
        //        eventListener.onEvent(event);
        //      } else {
        //        // try to map into any known object
        //        try {
        //          event = context.jsonMapper.map(body);
        //          eventListener.onEvent(event);
        //        } catch (Exception e) {
        //          LOG.error("STOMP message received but unable to convert: ", body, "; ", e.getMessage());
        //        }
        //      }
        //    }
        //  }

        //  @Override
        //  public void onDisconnect() {
        //    if (eventListener != null) {
        //      eventListener.onEvent(StompEvents.STOMP_DISCONNECTED);
        //    }
        //  }
        //}



        //protected class AppDestination extends Destination {
        //  string appId;

        //  public AppDestination(string appId) {
        //    super(null);
        //    this.appId = appId;
        //  }

        //  public string toString() {
        //    return configuration.basicDestination + "app:" + action;
        //  }
        //}

        /**
     * STOMP configuration. Supported properties are:
     * <p/>
     * - stomp.host (connect.secucard.com), STOMP host.<br/>
     * - stomp.virtualHost (null), STOMP virtual host.<br/>
     * - stomp.port (61614), STOMP port.<br/>
     * - stomp.destination (/exchange/connect.api), Base path of the secucard STOMP API.<br/>
     * - stomp.ssl (true), SSL used for STOMP or not<br/>
     * - stomp.user (null), Login, just for tests.<br/>
     * - stomp.pwd (null), Password, just for tests.<br/>
     * - stomp.replyQueue (/temp-queue/main), The default queue for all STOMP messages.<br/>
     * - stomp.messageTimeoutSec (120), Timeout for awaiting message receipts and also message responses.
     * An error is raised after. 0 means no waiting.<br/>
     * - stomp.connectTimeoutSec (20), Timeout for trying to connect to STOMP server. 0 means no waiting.<br/>
     * - stomp.socketTimeoutSec (10), Max time the receiving socket is allowed to block when waiting for any input.
     * This timeout mainly determines the time needed to detect broken socket connections, so short timeouts are desirable
     * but obviously also increases number of unnecessary performed timeout handling circles.<br/>
     * - stomp.heartbeatSec (30), The interval in sec a heart beat signal is sent to the Stomp server to verify the
     * client is still alive. Helps to cleanup connections to dead clients.<br/>
     * - stomp.maxMessageAgeSec (360), Max age of received STOMP messages in the systems message box before they get
     * deleted. Keeps the message queue clean, usually messages should not get very old in the box, if a message
     * reaches this max age its very likely that nobody is interested or a problem exist and therefore we can remove.<br/>
     * - stomp.disconnectOnError (true), STOMP channel will be disconnected or not when a ERROR frame was received
     * In our environment receiving an error means a non recoverable error condition caused by bugs or configuration problems,
     * so it's better to close this automatically to prevent resource leaking.
     */
        //public static class Configuration {
        //  private final String host;
        //  private final int port;
        //  private final String password;
        //  private final String virtualHost;
        //  private final int heartbeatSec;
        //  private final boolean useSsl;
        //  private final String userId;
        //  private final String replyQueue;
        //  private final int connectionTimeoutSec;
        //  private final int messageTimeoutSec;
        //  private final int maxMessageAgeSec;
        //  private final int socketTimeoutSec;
        //  private final String basicDestination;

        //  public Configuration(Properties properties) {
        //    this.host = properties.getProperty("stomp.host", "connect.secucard.com");
        //    this.port = Integer.parseInt(properties.getProperty("stomp.port", "61614"));
        //    this.password = properties.getProperty("stomp.pwd");
        //    this.virtualHost = properties.getProperty("stomp.virtualHost");
        //    this.heartbeatSec = Integer.parseInt(properties.getProperty("stomp.heartbeatSec", "30"));
        //    this.useSsl = bool.parseBoolean(properties.getProperty("stomp.ssl", "true"));
        //    this.userId = properties.getProperty("stomp.user");
        //    this.replyQueue = properties.getProperty("stomp.replyQueue", "/temp-queue/main");
        //    this.connectionTimeoutSec = Integer.parseInt(properties.getProperty("stomp.connectTimeoutSec", "20"));
        //    this.messageTimeoutSec = Integer.parseInt(properties.getProperty("stomp.messageTimeoutSec", "120"));
        //    this.maxMessageAgeSec = Integer.parseInt(properties.getProperty("stomp.maxMessageAgeSec", "360"));
        //    this.socketTimeoutSec = Integer.parseInt(properties.getProperty("stomp.socketTimeoutSec", "10"));

        //    string property = properties.getProperty("stomp.destination", "/exchange/connect.api");
        //    if (!property.endsWith("/")) {
        //      property += "/";
        //    }
        //    this.basicDestination = property;
        //  }


        //  @Override
        //  public string toString() {
        //    return "STOMP Configuration{" +
        //        "host='" + host + '\'' +
        //        ", port=" + port +
        //        ", password='" + password + '\'' +
        //        ", virtualHost='" + virtualHost + '\'' +
        //        ", heartbeatSec=" + heartbeatSec +
        //        ", useSsl=" + useSsl +
        //        ", userId='" + userId + '\'' +
        //        ", replyQueue='" + replyQueue + '\'' +
        //        ", connectionTimeoutSec=" + connectionTimeoutSec +
        //        ", messageTimeoutSec=" + messageTimeoutSec +
        //        ", maxMessageAgeSec=" + maxMessageAgeSec +
        //        ", socketTimeoutSec=" + socketTimeoutSec +
        //        ", basicDestination='" + basicDestination + '\'' +
        //        '}';
        //  }
        //}
    }
}
