namespace Secucard.Connect
{
    using Secucard.Connect.Auth;
    using Secucard.Model.Auth;

    public delegate void SecucardConnectEvent(object sender, SecucardConnectEventArgs args);

    public class SecucardConnectEventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthProviderStatusEnum Status { get; set; }
    }
}