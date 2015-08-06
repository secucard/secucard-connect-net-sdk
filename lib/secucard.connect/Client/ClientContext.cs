namespace Secucard.Connect.Client
{
    using System.Collections.Generic;
    using Secucard.Connect.auth;
    using Secucard.Connect.Net;
    using Secucard.Connect.Trace;

    public class ClientContext
    {
        internal TokenManager TokenManager;
        //protected EventDispatcher eventDispatcher;
        internal Dictionary<string, Channel> Channels; 
        public string DefaultChannel { get; set; }
        public ISecucardTrace SecucardTrace;
        internal string AppId;

        public ClientContext()
        {
            Channels = new Dictionary<string, Channel>();
        }
    }


}

