namespace Secucard.Connect
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.auth;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Rest;
    using Secucard.Stomp;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public const string VERSION = "0.1.development"; 

        internal ClientConfiguration Configuration;
        internal bool IsConnected { get; set; }

        public event SecucardConnectEvent SecucardConnectEvent;

        internal ClientContext Context;

        internal Dictionary<string, IService> Services;

        // provide service instances for easy access ------------------------------------------------------------------------

        //public Document document;
        //public General general;
        //public Payment payment;
        //public Loyalty loyalty;
        //public Services services;
        //public Smart smart;



        #region ### Start / Stop ###

        public void Connect()
        {
            // Start authentification
            Context.TokenManager.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Context.TokenManager.GetToken(false);

            //TODO: Start Stomp

            IsConnected = true;

            // TODO:Fire Event Connected
        }

        public void CancelAuth()
        {
        }

        public void Disconnect()
        {
           //TODO: Teardown
        }

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthProviderStatusUpdateEventArgs args)
        {
            // Send Events vom Auth Provider 
            if(SecucardConnectEvent!=null) 
                SecucardConnectEvent.Invoke(this,new SecucardConnectEventArgs {Status = args.Status,DeviceAuthCodes = args.DeviceAuthCodes});
        }

        #endregion

        #region ### Factory Client ###

        private SecucardConnect(){}

        public static SecucardConnect Create(ClientConfiguration config)
        {
            if (config == null) 
                config = ClientConfiguration.GetDefault();

            if(config.DataStorage==null)
                throw new Exception("Missing cache implementation found in config.");

            var client = new SecucardConnect {Configuration = config};

            var context = new ClientContext
            {
                AppId = config.AppId,
                SecucardTrace = config.SecucardTrace
            };
            // context.DataStorage = dataStorage;

            client.Context = context;

            AuthConfig authConfig = config.AuthConfig;
            StompConfig stompConfig = config.StompConfig;
            RestConfig restConfig = config.RestConfig;

            //    LOG.info("Creating client with configuration: ", config, "; ", authCfg, "; ", stompCfg, "; ", restConfig);
            if (config.ClientAuthDetails == null)
            {
                //TODO:
            }
            context.DefaultChannel = config.DefaultChannel;

            var restChannel = new RestChannel(restConfig, context);
            context.Channels.Add(ChannelOptions.CHANNEL_REST, restChannel);

            if (config.StompEnabled)
            {
                var sc = new StompChannel(stompConfig, context);
                context.Channels.Add(ChannelOptions.CHANNEL_STOMP, sc);
            }

            // TODO: Setup Event Listener for channels
            // TODO: Setup Event Dispater for incoming events from channels

            var restAuth = new RestAuth(authConfig)
            {
                UserAgentInfo = "secucardconnect-net-" + VERSION + "/net:" + Environment.OSVersion + " " + Environment.Version
            };
            context.TokenManager = new TokenManager(authConfig, config.ClientAuthDetails, restAuth) {Context = context};

            client.Services =  ServiceFactory.CreateServices(context);

            // TODO: WireServiceInstance

            return client;
        }

        #endregion


        public T GetService<T>()
        {
            return (T)Services[typeof (T).Name];
        }
    }
}
