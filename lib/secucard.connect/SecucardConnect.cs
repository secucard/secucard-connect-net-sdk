using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secucard.Connect
{
    using Secucard.Connect.auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Product;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public bool IsConnected { get; set; }

        public event SecucardConnectEvent SecucardConnectEvent;

        private ClientContext Context;

        #region ### Start / Stop ###

        public void Connect()
        {
            // Start Authentification
            Context.AuthProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Context.AuthProvider.GetToken(false);

            //TODO: Start Stomp

            IsConnected = true;

            // TODO:Fire Event Connected
        }

        public void CancelAuth()
        {
        }

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthProviderStatusUpdateEventArgs args)
        {
            // Send Events vom Auth Provider 
            if(SecucardConnectEvent!=null) 
                SecucardConnectEvent.Invoke(this,new SecucardConnectEventArgs {Status = args.Status,DeviceAuthCodes = args.DeviceAuthCodes});
        }

        #endregion

        #region ### Factory Client ###

        private SecucardConnect(ClientContext context)
        {
            this.Context = context;
        }


        public static SecucardConnect Create(string id, ClientConfiguration config, DataStorage dataStorage, ISecucardTrace secucardTrace)
        {
            // Factory
            ClientContext context = new ClientContext(id, config, dataStorage, secucardTrace);



            return new SecucardConnect(context);
        }

        #endregion

        #region ### Factory Service ###

        public T GetService<T>() where T : ServiceBase
        {
            var service = (T)Activator.CreateInstance(typeof(T));

            service.RestChannel = Context.RestChannel;

            return service;
        }

        #endregion

    }


}
