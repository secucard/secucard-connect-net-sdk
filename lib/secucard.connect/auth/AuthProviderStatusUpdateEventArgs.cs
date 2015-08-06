namespace Secucard.Connect.auth
{
    using System;
    using Secucard.Connect.auth.Model;

    public class AuthProviderStatusUpdateEventArgs : EventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthProviderStatusEnum Status { get; set; }
        public Token Token { get; set; }
    }
}