namespace Secucard.Connect.Net.Stomp
{
    using System;
    using System.Linq;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    public class StompRequest
    {
        public string Destination { get; set; }
        public string CorrelationId { get; set; }
        public string Body { get; set; }
        public string ReplayTo { get; set; }
        public string AppId { get; set; }

        /// <summary>
        ///     Factory method to convert channelRequest to stompRequest
        /// </summary>
        public static StompRequest Create(ChannelRequest channelRequest, string channelId, string replyTo, string destinationBase)
        {
            var stompRequest = new StompRequest
            {
                AppId = channelRequest.AppId,
                CorrelationId = channelId + "-" + Guid.NewGuid() + "-" + DateTime.Now.Ticks,
                ReplayTo = replyTo,
                Destination = CreateDestination(channelRequest, destinationBase),
                Body = CreateMessageBody(channelRequest)
            };

            return stompRequest;
        }

        private static string CreateMessageBody(ChannelRequest channelRequest)
        {
            var message = new Message
            {
                Pid = channelRequest.ObjectId,
                Query = JsonSerializer.SerializeJson(channelRequest.QueryParams),
                Data = JsonSerializer.SerializeJson(channelRequest.Object)
            };
            if (channelRequest.ActionArgs.Any()) message.Sid = channelRequest.ActionArgs.First();

            return JsonSerializer.SerializeJson(message).Trim();
        }

        private static string CreateDestination(ChannelRequest channelRequest, string destinationBase)
        {
            var destination = destinationBase + "api:" + GetCommand(channelRequest.Method);

            if (!string.IsNullOrWhiteSpace(channelRequest.Product))
                destination += string.Format("{0}.{1}", channelRequest.Product, channelRequest.Resource);

            if (!string.IsNullOrWhiteSpace(channelRequest.Action)) destination += "." + channelRequest.Action;

            return destination;
        }

        private static string GetCommand(ChannelMethod method)
        {
            switch (method)
            {
                case ChannelMethod.GET:
                    return "get:";
                case ChannelMethod.CREATE:
                    return "add:";
                case ChannelMethod.EXECUTE:
                    return "exec:";
                case ChannelMethod.UPDATE:
                    return "update:";
                case ChannelMethod.DELETE:
                    return "delete:";
                default:
                    throw new InvalidOperationException("Invalid channelMethod");
            }
        }
    }
}