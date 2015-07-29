namespace Secucard.Connect
{
    using System.Reflection.Emit;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    public class ClientContext
    {
        public const string STOMP = "stomp";
        public const string REST = "rest";

        internal DataStorage DataStorage;
        internal ISecucardTrace SecucardTrace;
        internal IChannel RestChannel;
        internal IChannel StompChannel;
        internal AuthProvider AuthProvider;
        internal ClientConfiguration Config;
        internal string ClientId;
        //internal ExceptionHandler exceptionHandler;
        //internal object RuntimeContext;
        //protected ResourceDownloader resourceDownloader;
        //protected EventDispatcher eventDispatcher;

        public ClientContext(string clientId, ClientConfiguration config, DataStorage dataStorage, ISecucardTrace secucardTrace)
        {
            ClientId = clientId;
            Config = config;
            Init(clientId, config, dataStorage,secucardTrace);
        }

   //     /**
   //* Return a channel to the given name.
   //*
   //* @param name The channel name or null for default channel.
   //*             Valid names are: {@link ClientContext#STOMP}, {@link ClientContext#REST}.
   //* @return Null if the requested channel is not available or disabled by config, the channel instance else.
   //* @throws java.lang.IllegalArgumentException if the name is not valid.
   //*/
   //     public IChannel GetChannel(string name)
   //     {
   //         if (name == null)
   //         {
   //             name = REST;// config.getDefaultChannel();
   //         }
   //         switch (name)
   //         {
   //             case REST:
   //                 return RestChannel;
   //             case STOMP:
   //                 return StompChannel;
   //             default:
   //                 return null;
   //             //throw new IllegalArgumentException("invalid channel name " + name);
   //         }
   //     }

        private void Init(string clientId, ClientConfiguration config, DataStorage dataStorage, ISecucardTrace secucardTrace)
        {
            SecucardTrace = secucardTrace;
            DataStorage = dataStorage;
            ClientId = clientId;
            Config = config;

            if (dataStorage == null)
            {
                // In case no storage passed then goto memory storage

                if (config.CacheDir == null)
                {
                    dataStorage = new MemoryDataStorage();
                }
                else
                {
                    // TODO: DiskDataStorage Class that save data to disk in various files.

                    //try
                    //{
                    //    dataStorage = new DiskCache(config.CacheDir + File.separator + clientId);
                    //}
                    //catch (IOException e)
                    //{
                    //    throw new SecuException("Error creating file storage: " + config.getCacheDir(), e);
                    //}
                }
            }

            // TODO: TRACE
            AuthProvider = new AuthProvider(clientId, config.AuthConfig, secucardTrace, dataStorage);
            RestChannel = new RestChannel(config.RestConfig, clientId, AuthProvider);

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



            //this.eventDispatcher = new EventDispatcher();
        }
    }
}

