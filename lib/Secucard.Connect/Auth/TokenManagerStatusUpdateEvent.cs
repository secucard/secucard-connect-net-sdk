namespace Secucard.Connect.Auth
{
    using System;
    using Secucard.Connect.Auth.Model;

    public delegate void TokenManagerStatusUpdateEventHandler(object sender, TokenManagerStatusUpdateEventArgs args);

    public class TokenManagerStatusUpdateEventArgs : EventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthStatusEnum Status { get; set; }
        public Token Token { get; set; }
    }
}