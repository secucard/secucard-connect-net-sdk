namespace Secucard.Connect
{
    using System.Collections.Generic;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    public class ClientContext
    {
        public const string STOMP = "stomp";
        public const string REST = "rest";

        internal DataStorage DataStorage;
        internal ISecucardTrace SecucardTrace;
        internal Dictionary<string, Channel.Channel> Channels; 
        internal AuthProvider AuthProvider;
        internal ClientConfiguration Config;
        internal string ClientId;
        public string DefaultChannel { get; set; }

        //internal ExceptionHandler exceptionHandler;
        //internal object RuntimeContext;
        //protected ResourceDownloader resourceDownloader;
        //protected EventDispatcher eventDispatcher;

        public ClientContext(string clientId, ClientConfiguration config, DataStorage dataStorage, ISecucardTrace secucardTrace)
        {
            ClientId = clientId;
            Config = config;
            Channels = new Dictionary<string, Channel.Channel>();
            Init(clientId, config, dataStorage,secucardTrace);
            DefaultChannel = "rest";
        }


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
            var restChannel = new RestChannel(config.RestConfig, clientId, AuthProvider,this);
            Channels.Add("rest", restChannel);

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

