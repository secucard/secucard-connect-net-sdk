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

namespace Secucard.Connect.DemoApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Client;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Smart;
    using Secucard.Connect.Product.Smart.Event;
    using Secucard.Connect.Product.Smart.Model;
    using Secucard.Connect.Storage;

    internal class Program
    {
        private static ClientConfiguration clientConfiguration;

        private static void Main(string[] args)
        {
            // Load default properties
            var properties = Properties.Load("SecucardConnect.config");
            clientConfiguration = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new ClientAuthDetailsDeviceToBeImplemented(),
                DataStorage = new MemoryDataStorage()
            };


            var Client = SecucardConnect.Create(clientConfiguration);
            Client.AuthEvent += ClientOnAuthEvent;
            Client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;
            Client.Open();


            var checkinService = Client.Smart.Checkins;
            checkinService.CheckinEvent+=  CheckinEvent;



            var transactionService = Client.Smart.Transactions;
            var identService = Client.Smart.Idents;

            transactionService.TransactionCashierEvent += SmartTransactionCashierEvent;

            // select an ident
            var availableIdents = identService.GetList(null);
            if (availableIdents == null || availableIdents.Count == 0)
            {
                throw new Exception("No idents found.");
            }
            var ident = availableIdents.List.First(o => o.Id == "smi_1");
            ident.Value = "pdo28hdal";

            var selectedIdents = new List<Ident> {ident};

            var groups = new List<ProductGroup>
            {
                new ProductGroup {Id = "group1", Desc = "beverages", Level = 1}
            };

            var basket = new Basket();
            basket.AddProduct(new Product
            {
                Id = 1,
                ArticleNumber = "3378",
                Ean = "5060215249804",
                Desc = "desc1",
                Quantity = 5m,
                PriceOne = 1999,
                Tax = 7,
                Groups = groups
            });
            basket.AddProduct(new Product
            {
                Id = 2,
                ArticleNumber = "art2",
                Ean = "5060215249805",
                Desc = "desc2",
                Quantity = 1m,
                PriceOne = 999,
                Tax = 19,
                Groups = groups
            });
            basket.AddProduct(new Text {Id = 1, ParentId = 2, Desc = "text1"});
            basket.AddProduct(new Text {Id = 2, ParentId = 2, Desc = "text2"});

            var basketInfo = new BasketInfo {Sum = 1, Currency = "EUR"};

            var newTrans = new Transaction
            {
                BasketInfo = basketInfo,
                Basket = basket,
                Idents = selectedIdents,
                MerchantRef = "merchant21",
                TransactionRef = "transaction99"
            };

            var transaction = transactionService.Create(newTrans);

            // you may edit some transaction data and update
            //newTrans.MerchantRef = "merchant";
            //transaction.TransactionRef = "trans1";
            //transaction = transactionService.Update(transaction);

            var type = "demo"; // demo|auto|cash
            // demo instructs the server to simulate a different (random) transaction for each invocation of startTransaction

            // Start Transaction
            var result = transactionService.Start(transaction.Id, type);

            var b = transactionService.Cancel(transaction.Id);
        }


        /// <summary>
        ///     Handles device authentication. Enter pin thru web interface service
        ///     !!! This is development only !!!
        /// </summary>
        private static void ClientOnAuthEvent(object sender, AuthEventArgs args)
        {
            if (args.Status == AuthStatusEnum.Pending)
            {
                // Set pin via SMART REST (only development)

                var reqSmartPin = new RestRequest
                {
                    Host = new Uri(new AuthConfig(clientConfiguration.Properties).OAuthUrl).Host,
                    BodyJsonString =
                        JsonSerializer.SerializeJson(new SmartPin {UserPin = args.DeviceAuthCodes.UserCode})
                };

                reqSmartPin.Header.Add("Authorization", "Bearer p11htpu8n1c6f85d221imj8l20");
                var restSmart =
                    new RestService(new RestConfig { Url = "https://core-dev10.secupay-ag.de/app.core.connector/api/v2/Smart/Devices/SDV_2YJDXYESB2YBHECVB5GQGSYPNM8UA6/pin" });
                var response = restSmart.RestPut(reqSmartPin);
            }
        }

        /// <summary>
        ///     Handles connect and disconnect events
        /// </summary>
        private static void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            Debug.WriteLine("Client Connected={0}", args.Connected);
        }

        /// <summary>
        /// Handles events during transaction
        /// </summary>
        private static void SmartTransactionCashierEvent(object sender, TransactionCashierEventArgs args)
        {
            Debug.WriteLine(args.SecucardEvent.Data.Text);
        }


        /// <summary>
        /// Gets called on new checkins
        /// </summary>
        private static void CheckinEvent(object sender, CheckinEventEventArgs args)
        {
            Debug.WriteLine(args.SecucardEvent.Data.CustomerName);
        }


        /// <summary>
        /// Ownn 
        /// </summary>
        private class ClientAuthDetailsDeviceToBeImplemented : AbstractClientAuthDetails, IClientAuthDetails
        {
            public OAuthCredentials GetCredentials()
            {
                return new DeviceCredentials("611c00ec6b2be6c77c2338774f50040b",
                    "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb", "/vendor/unknown/cashier/dotnettest1");
            }

            public ClientCredentials GetClientCredentials()
            {
                return (ClientCredentials)GetCredentials();
            }
        }


    }
}