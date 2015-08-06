﻿namespace Secucard.Connect.Client
{
    public delegate void ConnectionStateChangedEventHandler(object sender, ConnectionStateChangedEventArgs args);

    public class ConnectionStateChangedEventArgs
    {
        public bool Connected { get; set; }
    }
}