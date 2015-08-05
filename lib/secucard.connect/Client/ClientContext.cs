namespace Secucard.Connect
{
    using System.Collections.Generic;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Trace;

    public class ClientContext
    {
        internal TokenManager TokenManager;
        //protected EventDispatcher eventDispatcher;
        internal Dictionary<string, Channel.Channel> Channels; 
        public string DefaultChannel { get; set; }
        public ISecucardTrace SecucardTrace;
        internal string AppId;

        public ClientContext()
        {
            Channels = new Dictionary<string, Channel.Channel>();
        }
    }


}

