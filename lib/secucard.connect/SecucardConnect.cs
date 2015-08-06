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
namespace Secucard.Connect
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp;
    using Secucard.Connect.Rest;
    using Secucard.Stomp;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public const string VERSION = "0.1.development";

        public event AuthEvent AuthEvent;
        public event ConnectionStateChangedEvent ConnectionStateChangedEvent;

        internal bool IsConnected { get; set; }

        private ClientConfiguration Configuration;
        private ClientContext Context;
        private Dictionary<string, IService> Services;

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
            // Start authentication
            Context.TokenManager.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Context.TokenManager.GetToken(true);

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


        #endregion


        #region ### Events ###

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthManagerStatusUpdateEventArgs args)
        {
            // Send Events vom Auth Provider an pass it up to client
            OnAuthEvent(new AuthEventArgs { Status = args.Status, DeviceAuthCodes = args.DeviceAuthCodes });
        }

        private void OnAuthEvent(AuthEventArgs args)
        {
            if (AuthEvent != null) AuthEvent(this, args);
        }

        private void OnConnectionStateChangedEvent(ConnectionStateChangedEventArgs args)
        {
            if (ConnectionStateChangedEvent != null) ConnectionStateChangedEvent(this, args);
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
