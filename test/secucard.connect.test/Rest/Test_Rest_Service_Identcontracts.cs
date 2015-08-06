﻿namespace secucard.connect.test.Rest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Service.Model.services;
    using Secucard.Connect.rest;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Services_IdentContracts : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Services_Identcontracts_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "Services/Identcontracts",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<IdentContract>(request);

            Assert.IsTrue(data.Count > 0);
        }

        
    }
}