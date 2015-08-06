namespace Secucard.Stomp.test
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Test_StompClient : Test_Base
    {
        [TestMethod]
        [TestCategory("stomp")]
        public void Test_StompClient_Connect()
        {
            using (var client = new StompClient(Config))
            {
                var connect = client.Connect();
                Assert.IsTrue(connect);
                Assert.AreEqual(client.StompClientStatus, EnumStompCoreStatus.Connected);

                var framePing = new StompFrame(StompCommands.SEND);
                framePing.Headers.Add(StompHeader.UserId, Config.Login);
                framePing.Headers.Add(StompHeader.Destination, "/exchange/connect.api/ping");
                framePing.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                framePing.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");

                framePing.Body = "Test";

                StompFrame frameIn = null;
                client.StompClientFrameArrived += (sender, args) => { frameIn = args.Frame; };
                client.SendFrame(framePing);

                // waiting for frame to come
                while (frameIn == null){}
                Assert.IsTrue(frameIn.Body.Contains("Test"));

                
                frameIn = null;

                var frameRefresh = new StompFrame(StompCommands.SEND);
                frameRefresh.Headers.Add(StompHeader.UserId, Config.Login);
                frameRefresh.Headers.Add(StompHeader.Destination, "/exchange/connect.api/api:exec:auth.sessions.refresh");
                frameRefresh.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                frameRefresh.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");
                frameRefresh.Body = "{ \"pid\":\"me\"}";
                client.SendFrame(frameRefresh);
                while (frameIn == null) { }
                Assert.IsTrue(frameIn.Body.Contains("Test"));


                // check out heartbeat in trace
                Thread.Sleep(6000);

                client.Disconnect();
                Thread.Sleep(3000); // Wait for Disconnect Receipt to arrive
                Assert.IsTrue(client.StompClientStatus == EnumStompCoreStatus.Disconnected);
            }
        }

        [TestMethod]
        [TestCategory("stomp")]
        public void Test_StompClient_Connect_Dbl()
        {
            using (var client = new StompClient(Config))
            {
                var connect1 = client.Connect();
                Assert.IsTrue(connect1);
                var connect2 = client.Connect();
                Assert.IsTrue(connect2);
                Assert.AreEqual(client.StompClientStatus, EnumStompCoreStatus.Connected);
            }
        }
    }
}