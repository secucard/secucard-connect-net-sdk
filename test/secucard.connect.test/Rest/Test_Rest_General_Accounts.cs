﻿namespace secucard.connect.test.Rest
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.rest;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Accounts : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Accounts_1_GET()
        {
            var request = new RestRequest
           {
               Token = Token,
               QueyParams = new QueryParams
               {
                   Count = 10,
                   Offset = 0
               },
               PageUrl = "General/Accounts",
               Host = "core-dev10.secupay-ag.de"
           };

            var data = RestService.GetList<Account>(request);

            Assert.IsTrue(data.Count > 0);
        }
    }
}