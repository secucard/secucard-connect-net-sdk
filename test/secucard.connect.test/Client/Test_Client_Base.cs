namespace Secucard.Connect.Test.Client
{
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    public class Test_Client_Base
    {
        protected const string storagePath = "data\\secucard.sec";
        private readonly ClientConfiguration ClientConfigurationDevice;
        private readonly ClientConfiguration ClientConfigurationUser;
        private readonly MemoryDataStorage Storage;
        protected readonly ISecucardTrace Tracer;
        protected SecucardConnect Client;

        protected Test_Client_Base()
        {
            Tracer = new SecucardTraceMemory();
            Storage = new MemoryDataStorage();

            ClientConfigurationDevice = ClientConfiguration.Get(); // Load Defaults
            ClientConfigurationDevice.ClientAuthDetails = new ClientAuthDetailsDeviceTest();
            ClientConfigurationDevice.DataStorage = Storage;

            ClientConfigurationUser = ClientConfiguration.Get();
            ClientConfigurationUser.ClientAuthDetails = new ClientAuthDetailsUserTest();
            ClientConfigurationUser.DataStorage = Storage;
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
            Tracer.Info("ClientOnSecucardConnectEvent Status={0}", args.Status);

            if (args.Status == AuthStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = ClientConfigurationDevice.Properties.Get("Auth.Host"),
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin {UserPin = args.DeviceAuthCodes.UserCode})
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart =
                    new RestService(
                        "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin");
                var response = restSmart.RestPut(reqSmartPin);
                Assert.IsTrue(response.Length > 0);
            }
        }

        /// <summary>
        ///     Handles connect and disconnect events
        /// </summary>
        private void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            Tracer.Info("Client Connected={0}", args.Connected);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Client != null) Client.Close();
            //Debug.WriteLine((Tracer as SecucardTraceMemory).GetAllTrace());
        }
    }
}