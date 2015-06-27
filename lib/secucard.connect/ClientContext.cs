/**
 * Context instance holding all necessary beans used in client.
 */

using System;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using Secucard.Connect.Auth;
using Secucard.Connect.Storage;

namespace Secucard.Connect
{
    using Secucard.Connect.Channel;
    using Secucard.Connect.Channel.Rest;

    public class ClientContext
    {
        public const string STOMP = "stomp";
        public const string REST = "rest";

        protected DataStorage DataStorage;
        protected IChannel RestChannel;
        protected IChannel StompChannel;
        protected AuthProvider AuthProvider;
        protected ClientConfiguration Config;
        protected string ClientId;
        protected ExceptionHandler exceptionHandler;
        protected object RuntimeContext;
        //protected ResourceDownloader resourceDownloader;
        //protected EventDispatcher eventDispatcher;

        public ClientContext(string clientId, ClientConfiguration config, object runtimeContext, DataStorage dataStorage, string clientId1, ClientConfiguration config1)
        {
            ClientId = clientId1;
            Config = config1;
            Init(clientId, config, runtimeContext, dataStorage);
        }

        //     /**
        //* Obtain the current client context instance..
        //*/
        //     public static ClientContext get() {
        //         return (ClientContext) ThreadLocalUtil.get(ClientContext.class.getName());
        //     }



        /**
   * Return a channel to the given name.
   *
   * @param name The channel name or null for default channel.
   *             Valid names are: {@link ClientContext#STOMP}, {@link ClientContext#REST}.
   * @return Null if the requested channel is not available or disabled by config, the channel instance else.
   * @throws java.lang.IllegalArgumentException if the name is not valid.
   */
        public IChannel GetChannel(string name)
        {
            if (name == null)
            {
                name = REST;// config.getDefaultChannel();
            }
            switch (name)
            {
                case REST:
                    return RestChannel;
                case STOMP:
                    return StompChannel;
                default:
                    return null;
                //throw new IllegalArgumentException("invalid channel name " + name);
            }
        }

        /**
   * Initialize beans in this context and wiring up dependencies.
   * Checks for androidMode config property and does special initialization.
   */

        private void Init(string clientId, ClientConfiguration config, object runtimeContext, DataStorage dataStorage)
        {
            //AuthProvider authProvider;
            //RestChannelBase restChannel;

            DataStorage = dataStorage;

            if (dataStorage == null)
            {
                //if (config.getCacheDir() == null) {
                //    dataStorage = new MemoryDataStorage();
                //} else {
                //    try {
                //        dataStorage = new DiskCache(config.getCacheDir() + File.separator + clientId);
                //    } catch (IOException e) {
                //        throw new SecuException("Error creating file storage: " + config.getCacheDir(), e);
                //    }
                //}
            }

            //    restChannel = new RestChannel(clientId, config.getRestConfiguration());
            //    authProvider = new AuthProvider(clientId, config,null,dataStorage);


            //authProvider.setRestChannel(restChannel);
            //this.authProvider = authProvider;

            //restChannel.setAuthProvider(this.authProvider);
            //this.restChannel = restChannel;

            ////ResourceDownloader downloader = ResourceDownloader.get();
            ////downloader.setCache(dataStorage);
            ////downloader.setHttpClient(restChannel);
            ////this.resourceDownloader = downloader;

            //StompChannel sc = null;
            //if (config.stompEnabled) {
            //    sc = new StompChannel(clientId, config.getStompConfiguration());
            //    sc.setAuthProvider(this.authProvider);
            //}
            //this.stompChannel = sc;

            ClientId = clientId;
            Config = config;
            this.RuntimeContext = runtimeContext;

            //this.eventDispatcher = new EventDispatcher();
        }
    }
}

