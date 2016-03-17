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
    public class Test_StompCore : Test_Base
    {
        [TestMethod]
        [TestCategory("stomp")]
        public void Test_Core_Connect()
        {
            IsConnected = false;
            MessageReceived = false;

            var frame = new StompFrame(StompCommands.Connect);

            frame.Headers.Add(StompHeader.Login, AccessToken);
            frame.Headers.Add(StompHeader.Passcode, AccessToken);
            frame.Headers.Add(StompHeader.HeartBeat,
                string.Format("{0},{1}", StompConfig.HeartbeatMs, StompConfig.HeartbeatMs));
            frame.Headers.Add(StompHeader.AcceptVersion, StompConfig.AcceptVersion);

            using (var core = new StompCore(StompConfig))
            {
                core.Init();
                core.StompCoreFrameArrivedEvent += ClientOnStompCoreFrameArrived;
                core.SendFrame(frame);

                while (!IsConnected)
                {
                }

                var framePing = new StompFrame(StompCommands.Send);
                framePing.Headers.Add(StompHeader.UserId, AccessToken);
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
            if (frame.Command == StompCommands.Connected) IsConnected = true;
            if (frame.Command == StompCommands.Message) MessageReceived = true;
        }
    }
}