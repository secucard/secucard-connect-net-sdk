namespace Secucard.Connect.Test.Rest
{
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Stores : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_1_GET()
        {
            var request = new RestRequest
            {
                Method = WebRequestMethods.Http.Get,
                Token = AccessToken,
                QueryParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0,
                },
                PageUrl = "General/Stores",
                Host = "core-dev10.secupay-ag.de"
            };

            var list = RestService.GetList<Store>(request);

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_2_GET()
        {
            {
                var request = new RestRequest
                {
                    Method = WebRequestMethods.Http.Get,
                   Token = AccessToken,
                   Url = "https://core-dev10.secupay-ag.de/app.core.connector/ds_g/1822a1ccd76984b4a7e9536884b75fedf9e06a5a"
                };

                var stream = RestService.GetStream(request);

                Assert.IsNotNull(stream);
            }
        }

    }
}