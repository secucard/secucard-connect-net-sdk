namespace secucard.connect.test.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Channel.Rest;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Test;
    using Secucard.Connect.Test.Client;
    using Secucard.Connect.Trace;
    using Secucard.Model;
    using Secucard.Model.Smart;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_1 : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Service_General_Skeleton_1_GET()
        {
            SecucardConnect client = SecucardConnect.Create("id", ClientConfigurationDevice, Storage, Tracer);
            client.SecucardConnectEvent += ClientOnSecucardConnectEvent;
            client.Connect();

            var service = client.GetService<GeneralSkeletonsService>();

            var queryParams = new QueryParams
            {
                Count = 10,
                ScrollExpire = "5m",
                SortOrder = new NameValueCollection { { "a", QueryParams.SORT_ASC } },
                Fields = new List<string> { "id", "a", "b" }
            };

            var list = service.GetSkeletons(queryParams);
            Assert.IsTrue(list.Count > 0);

            var skeleton = service.GetSkeleton(list.List.First().Id);
            Assert.AreEqual(skeleton.A, "abc1");

            Console.WriteLine((Tracer as SecucardTraceMemory).GetAllTrace());
        }

        ///// <summary>
        ///// Handles Device Authentification. Enter pin thru web interface service
        ///// </summary>
        //private void ClientOnSecucardConnectEvent(object sender, SecucardConnectEventArgs args)
        //{
        //    if (args.Status == AuthProviderStatusEnum.Pending)
        //    {
        //        // Set pin via SMART REST (only development)

        //        var reqSmartPin = new RestRequest
        //        {
        //            Host = ClientConfigurationDevice.AuthConfig.Host,
        //            BodyJsonString = JsonSerializer.SerializeJson(new SmartPin { UserPin = args.DeviceAuthCodes.UserCode })
        //        };

        //        reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
        //        var restSmart = new RestService(new RestConfig { BaseUrl = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
        //        var response = restSmart.RestPut(reqSmartPin);
        //        Assert.IsTrue(response.Length > 0);
        //    }
        //}


    }
}