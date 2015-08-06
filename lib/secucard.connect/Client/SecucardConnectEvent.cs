namespace Secucard.Connect.Client
{
    using Secucard.Connect.auth;
    using DeviceAuthCode = Secucard.Connect.auth.Model.DeviceAuthCode;

    public delegate void SecucardConnectEvent(object sender, SecucardConnectEventArgs args);

    public class SecucardConnectEventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthProviderStatusEnum Status { get; set; }
    }
}