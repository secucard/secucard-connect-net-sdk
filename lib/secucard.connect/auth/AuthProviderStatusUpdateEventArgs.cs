namespace Secucard.Connect.auth
{
    using System;
    using Secucard.Connect.Auth;
    using Secucard.Model.Auth;
    using DeviceAuthCode = Secucard.Connect.auth.Model.DeviceAuthCode;

    public class AuthProviderStatusUpdateEventArgs : EventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthProviderStatusEnum Status { get; set; }
    }
}