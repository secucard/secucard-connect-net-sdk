namespace Secucard.Connect
{
    using System;
    using Secucard.Connect.auth;
    using Secucard.Connect.Product;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Stomp;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public string Version { get { return "0.1.development"; } }

        public bool IsConnected { get; set; }

        public event SecucardConnectEvent SecucardConnectEvent;

        private ClientContext Context;

        #region ### Start / Stop ###

        public void Connect()
        {
            // Start authentification
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

        public T GetService<T>() where T : AbstractService
        {
            var service = (T)Activator.CreateInstance(typeof(T));

            service.Context = Context;

            // TODO: Service needs to register for events from stomp

            return service;
        }

        #endregion

    }


}
