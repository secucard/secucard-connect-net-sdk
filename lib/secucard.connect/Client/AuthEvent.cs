namespace Secucard.Connect.Client
{
    using Secucard.Connect.Auth;
    using DeviceAuthCode = Secucard.Connect.Auth.Model.DeviceAuthCode;

    public delegate void AuthEvent(object sender, AuthEventArgs args);

    public class AuthEventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthStatusEnum Status { get; set; }
    }
}