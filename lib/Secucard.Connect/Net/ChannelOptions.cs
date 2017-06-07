namespace Secucard.Connect.Net
{
    /// <summary>
    ///   Hold all options for the channel
    /// </summary>
    public class ChannelOptions
    {
        public const string ChannelRest = "rest";
        public const string ChannelStomp = "stomp";
        public bool Anonymous = false;
        public bool Expand = false;
        public string Channel;
        public string ClientId = null;
        public int? TimeOutSec;


        public static ChannelOptions GetDefault()
        {
            // TODO: Default Channel aus config
            return new ChannelOptions {Channel = ChannelRest, TimeOutSec = 100};
        }

        public ChannelOptions()
        {
        }

        public ChannelOptions(string channel)
        {
            Channel = channel;
        }
    }
}