namespace Secucard.Connect.Product
{
    using Secucard.Connect.Channel;
    using Secucard.Connect.Channel.Rest;

    public abstract class ServiceBase
    {
        public IChannel RestChannel { get; set; }

    }
}
