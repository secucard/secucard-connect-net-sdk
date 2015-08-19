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

namespace Secucard.Connect.Test.Client
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Storage;

    public class Test_Client_Base: Test_Base
    {
        private readonly ClientConfiguration ClientConfigurationDevice;
        private readonly ClientConfiguration ClientConfigurationUser;
        protected SecucardConnect Client;

        protected Test_Client_Base()
        {
            ClientConfigurationDevice = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new ClientAuthDetailsDeviceTest(),
                DataStorage =  new MemoryDataStorage()
            };

            ClientConfigurationUser = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new ClientAuthDetailsUserTest(),
                DataStorage = new MemoryDataStorage()
            };
        }

        protected void StartupClientDevice()
        {
            Client = SecucardConnect.Create(ClientConfigurationDevice);
            Client.AuthEvent += ClientOnAuthEvent;
            Client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;
            Client.Open();
        }

        protected void StartupClientUser()
        {
            Client = SecucardConnect.Create(ClientConfigurationUser);
            Client.AuthEvent += ClientOnAuthEvent;
            Client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;
            Client.Open();
        }

        /// <summary>
        ///     Handles device authentication. Enter pin thru web interface service
        /// </summary>
        private void ClientOnAuthEvent(object sender, AuthEventArgs args)
        {
            Trace.TraceInformation("ClientOnSecucardConnectEvent Status={0}", args.Status);

            if (args.Status == AuthStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = new Uri(AuthConfig.OAuthUrl).Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin { UserPin = args.DeviceAuthCodes.UserCode })
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart =
                    new RestService(new RestConfig { Url = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }

        /// <summary>
        ///     Handles connect and disconnect events
        /// </summary>
        private void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            Trace.TraceInformation("Client Connected={0}", args.Connected);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Client != null) Client.Close();
        }
    }
}