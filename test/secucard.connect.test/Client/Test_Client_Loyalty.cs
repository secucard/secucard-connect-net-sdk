namespace Secucard.Connect.Test.Client
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_Loyalty : Test_Client_Base
    {
        [TestMethod, TestCategory("Client")]
        public void Test_Client_LoyaltyCustomers_1()
        {
            StartupClientUser();

            var customerService = Client.Loyalty.Customers;
            customerService.GetList(null);
        }
    }
}