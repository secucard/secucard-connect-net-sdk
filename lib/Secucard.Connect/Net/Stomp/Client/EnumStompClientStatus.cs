namespace Secucard.Connect.Net.Stomp.Client
{
    public enum EnumStompClientStatus
    {
        NotConnected = 1,
        Connecting = 2,
        Connected = 3,
        Error = 4,
        Timeout = 5,
        Disconnecting = 6,
        Disconnected = 7
    }
}