namespace Secucard.Connect.Channel.Rest
{
    using System;

    public class UserAgentProvider
    {
        public static string GetValue()
        {
            // todo: add more info like android version
            return "connect client .NET v0.1 OS:" + Environment.OSVersion + " " + Environment.Version;
        }
    }
}