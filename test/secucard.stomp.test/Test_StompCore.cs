namespace Secucard.Stomp.test
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Test_StompCore : Test_Base
    {
        [TestMethod]
        [TestCategory("stomp")]
        public void Test_Core_Connect()
        {
            Connected = false;
            Message = false;

            var frame = new StompFrame(StompCommands.CONNECT);

            frame.Headers.Add(StompHeader.Login, Config.Login);
            frame.Headers.Add(StompHeader.Passcode, Config.Password);
            frame.Headers.Add(StompHeader.HeartBeat, string.Format("{0},{1}", Config.HeartbeatClientMs, Config.HeartbeatServerMs));
            frame.Headers.Add(StompHeader.AcceptVersion, Config.AcceptVersion);

            using (var core = new StompCore(Config))
            {
                core.Init();
                core.StompCoreFrameArrived += ClientOnStompCoreFrameArrived;
                core.SendFrame(frame);

                while (!Connected)
                {
                }

                var framePing = new StompFrame(StompCommands.SEND);
                framePing.Headers.Add(StompHeader.UserId, Config.Login);
                framePing.Headers.Add(StompHeader.Destination, "/exchange/connect.api/ping");
                framePing.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                framePing.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");

                framePing.Body = "Testdaten";
                core.SendFrame(framePing);


                while (!Message)
                {
                }
                Thread.Sleep(2000);
            }
        }

        private void ClientOnStompCoreFrameArrived(object sender, StompCoreFrameArrivedEventArgs args)
        {
            var frame = args.Frame;
            if (frame.Command == StompCommands.CONNECTED) Connected = true;
            if (frame.Command == StompCommands.MESSAGE) Message = true;
        }
    }
}