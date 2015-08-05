using System;

namespace Secucard.Connect.Net.Stomp
{
    public class StompMessage
    {
        public string Id;
        public string Body;
        public DateTime ReceiveTime;

        public StompMessage(string id, string body)
        {
            this.Id = id;
            this.Body = body;
            ReceiveTime = DateTime.Now;
        }
    }
}
