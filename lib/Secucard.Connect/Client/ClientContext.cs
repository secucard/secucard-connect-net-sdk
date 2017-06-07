namespace Secucard.Connect.Client
{
    using System.Collections.Generic;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Event;
    using Secucard.Connect.Net;

    public class ClientContext
    {
        public ClientContext()
        {
            Channels = new Dictionary<string, Channel>();
            EventDispatcher = new EventDispatcher();
        }

        internal TokenManager TokenManager { get; set; }
        internal EventDispatcher EventDispatcher { get; set; }
        internal Dictionary<string, Channel> Channels { get; set; }
        public string DefaultChannel { get; set; }
        internal string AppId { get; set; }
    }
}