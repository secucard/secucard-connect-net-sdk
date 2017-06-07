namespace Secucard.Connect.Net.Stomp
{
    using System;

    public class StompMessage
    {
        public string Id;
        public string Body;
        public DateTime ReceiveTime;

        public StompMessage(string id, string body)
        {
            Id = id;
            Body = body;
            ReceiveTime = DateTime.Now;
        }
    }
}