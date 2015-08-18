/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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