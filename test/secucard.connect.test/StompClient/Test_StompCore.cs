namespace Secucard.Connect.Test.StompClient
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Stomp.Client;

    [TestClass]
    public class Test_StompCore : Test_Base
    {
        [TestMethod]
        [TestCategory("stomp")]
        public void Test_Core_Connect()
        {
            IsConnected = false;
            MessageReceived = false;

            var frame = new StompFrame(StompCommands.CONNECT);

            frame.Headers.Add(StompHeader.Login, StompConfig.Login);
            frame.Headers.Add(StompHeader.Passcode, StompConfig.Password);
            frame.Headers.Add(StompHeader.HeartBeat, string.Format("{0},{1}", StompConfig.HeartbeatClientMs, StompConfig.HeartbeatServerMs));
            frame.Headers.Add(StompHeader.AcceptVersion, StompConfig.AcceptVersion);

            using (var core = new StompCore(StompConfig))
            {
                core.Init();
                core.StompCoreFrameArrived += ClientOnStompCoreFrameArrived;
                core.SendFrame(frame);

                while (!IsConnected)
                {
                }

                var framePing = new StompFrame(StompCommands.SEND);
                framePing.Headers.Add(StompHeader.UserId, StompConfig.Login);
                framePing.Headers.Add(StompHeader.Destination, "/exchange/connect.api/ping");
                framePing.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                framePing.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");

                framePing.Body = "Testdaten";
                core.SendFrame(framePing);


                while (!MessageReceived)
                {
                }
                Thread.Sleep(2000);
            }
        }

        private void ClientOnStompCoreFrameArrived(object sender, StompCoreFrameArrivedEventArgs args)
        {
            var frame = args.Frame;
            if (frame.Command == StompCommands.CONNECTED) IsConnected = true;
            if (frame.Command == StompCommands.MESSAGE) MessageReceived = true;
        }
    }
}