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
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Stomp.Client;

    [TestClass]
    public class Test_StompClient : Test_Base
    {
        [TestMethod]
        [TestCategory("stomp")]
        public void Test_StompClient_Connect()
        {
            using (var client = new StompClient(StompConfig))
            {
                var connect = client.Connect(AccessToken, AccessToken);
                Assert.IsTrue(connect);
                Assert.AreEqual(client.StompClientStatus, EnumStompClientStatus.Connected);

                var framePing = new StompFrame(StompCommands.Send);
                framePing.Headers.Add(StompHeader.UserId, AccessToken);
                framePing.Headers.Add(StompHeader.Destination, "/exchange/connect.api/ping");
                framePing.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                framePing.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");

                framePing.Body = "Test";

                StompFrame frameIn = null;
                client.StompClientFrameArrivedEvent += (sender, args) => { frameIn = args.Frame; };
                client.SendFrame(framePing);

                // waiting for frame to come
                while (frameIn == null)
                {
                }
                Assert.IsTrue(frameIn.Body.Contains("Test"));


                frameIn = null;

                var frameRefresh = new StompFrame(StompCommands.Send);
                frameRefresh.Headers.Add(StompHeader.UserId, AccessToken);
                frameRefresh.Headers.Add(StompHeader.Destination, "/exchange/connect.api/api:exec:auth.sessions.refresh");
                frameRefresh.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                frameRefresh.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");
                frameRefresh.Body = "{ \"pid\":\"me\"}";
                client.SendFrame(frameRefresh);
                while (frameIn == null)
                {
                }
                Assert.IsTrue(frameIn.Body.Contains("data"));

                frameIn = null;

                var frameSkeleton = new StompFrame(StompCommands.Send);
                frameSkeleton.Headers.Add(StompHeader.UserId, AccessToken);
                frameSkeleton.Headers.Add(StompHeader.Destination, "/exchange/connect.api/api:get:general.skeletons");
                frameSkeleton.Headers.Add(StompHeader.CorrelationId, Guid.NewGuid().ToString());
                frameSkeleton.Headers.Add(StompHeader.ReplyTo, "/temp-queue/main");
                //frameSkeleton.Body = "{ \"pid\":\"me\"}";
                client.SendFrame(frameSkeleton);
                while (frameIn == null)
                {
                }
                Assert.IsTrue(frameIn.Body.Contains("skeletons"));

                // check out heartbeat in trace
                Thread.Sleep(3000);

                client.Disconnect();
                Thread.Sleep(3000); // Wait for Disconnect Receipt to arrive
                Assert.IsTrue(client.StompClientStatus == EnumStompClientStatus.Disconnected);
            }
        }

        [TestMethod]
        [TestCategory("stomp")]
        public void Test_StompClient_Connect_Dbl()
        {
            using (var client = new StompClient(StompConfig))
            {
                var connect1 = client.Connect(AccessToken, AccessToken);
                Assert.IsTrue(connect1);
                var connect2 = client.Connect(AccessToken, AccessToken);
                Assert.IsTrue(connect2);
                Assert.AreEqual(client.StompClientStatus, EnumStompClientStatus.Connected);
            }
        }
    }
}