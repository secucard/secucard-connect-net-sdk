namespace Secucard.Connect.Test.StompClient
{
    using Secucard.Connect.Net.Stomp.Client;

  
    public class Test_Base
    {
        protected readonly StompConfig Config;
        protected bool Connected;
        protected bool Message;

        protected Test_Base()
        {
            //string host = "connect.secucard.com"; // 91.195.150.24
            var host = "dev10.secupay-ag.de"; // 91.195.151.211
            var port = 61614;
            Config = new StompConfig
            {
                Host = host,
                Port = port,
                Login = "39as2l3t1drqehkcdloua20t94",
                Password = "39as2l3t1drqehkcdloua20t94", 
                AcceptVersion = "1.2",
                HeartbeatClientMs = 5000,
                HeartbeatServerMs = 5000,
                Ssl = true
            };
        }
    }
}