namespace Secucard.Stomp.test
{
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
                Login = "b5005kdkm7tl0f9t9a6514b3i5",//
                Password = "b5005kdkm7tl0f9t9a6514b3i5", //fvv6vsup13frjpj8nr6gju0691
                AcceptVersion = "1.2",
                HeartbeatClientMs = 5000,
                HeartbeatServerMs = 5000,
                Ssl = true
            };
        }
    }
}