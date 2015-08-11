namespace Secucard.Connect.Test.StompClient
{
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Stomp.Client;

    [TestClass]
    public class Test_StompFrame : Test_Base
    {
        [TestMethod]
        [TestCategory("stomp")]
        public void Test_Frame_Serialization()
        {
            var frame = new StompFrame(StompCommands.CONNECT);

            frame.Headers.Add(StompHeader.Host, StompConfig.Host);
            frame.Headers.Add(StompHeader.Login, StompConfig.Login);
            frame.Headers.Add(StompHeader.Passcode, StompConfig.Password);
            frame.Headers.Add(StompHeader.HeartBeat,
                string.Format("{0},{1}", StompConfig.HeartbeatClientMs, StompConfig.HeartbeatServerMs));
            frame.Headers.Add(StompHeader.AcceptVersion, StompConfig.AcceptVersion);

            var msg = frame.GetFrame();
            var frame2 = StompFrame.CreateFrame(Encoding.UTF8.GetBytes(msg));

            Assert.AreEqual(frame.Command, frame2.Command, "Commands are not equal");
            Assert.AreEqual(frame.Headers[StompHeader.Host], frame2.Headers[StompHeader.Host], "Host are not equal");
        }
    }
}