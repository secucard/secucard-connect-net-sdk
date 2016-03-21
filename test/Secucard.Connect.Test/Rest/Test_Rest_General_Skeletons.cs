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

namespace Secucard.Connect.Test.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_General_Skeletons : Test_Rest_Base_AuthDevice
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_1_GET()
        {
            // {{host}}General/Skeletons?count=10&offset=5&fields=a,b&sort[a]=asc
            var request = new RestRequest
            {
                Method = WebRequestMethods.Http.Get,
                Token = AccessToken,
                QueryParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0,
                    SortOrder = new Dictionary<string, string>() {{"a", QueryParams.SortAsc}},
                    Fields = new List<string> {"a", "b"}
                },
                PageUrl = "General/Skeletons",
                Host = "core-dev10.secupay-ag.de"
            };

            var list = RestService.GetList<Skeleton>(request);

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_2_GET()
        {
            string scrollId;

            {
                // {{host}}General/Skeletons?count=10&scroll_expire=5m&fields=a,b&sort[a]=asc
                var request = new RestRequest
                {
                    Method = WebRequestMethods.Http.Get,
                    Token = AccessToken,
                    QueryParams = new QueryParams
                    {
                        Count = 10,
                        ScrollExpire = "5m",
                        SortOrder = new Dictionary<string, string>() {{"a", QueryParams.SortAsc}},
                        Fields = new List<string> {"a", "b"}
                    },
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };

                var list = RestService.GetList<Skeleton>(request);

                scrollId = list.ScrollId;

                Assert.IsNotNull(scrollId);
                Assert.IsTrue(list.Count > 0);
            }

            {
                // {{host}}General/Skeletons?scroll_id=xxxxx
                var request = new RestRequest
                {
                    Method = WebRequestMethods.Http.Get,
                    Token = AccessToken,
                    QueryParams = new QueryParams
                    {
                        ScrollId = scrollId
                    },
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };
                var list = RestService.GetList<Skeleton>(request);

                Assert.IsNotNull(list.ScrollId);
                Assert.IsTrue(list.List.Any());
            }
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_3_GET()
        {
            // {{host}}General/Skeletons?q=a:abc1? OR (b:*0 AND NOT c:???1??)
            var request = new RestRequest
            {
                Token = AccessToken,
                QueryParams = new QueryParams
                {
                    Query = "a:abc1? OR (b:*0 AND NOT c:???1??)"
                },
                PageUrl = "General/Skeletons",
                Host = "core-dev10.secupay-ag.de"
            };

            var list = RestService.GetList<Skeleton>(request);

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_4_GET()
        {
            // {{host}}General/Skeletons?expand=true
            var request = new RestRequest
            {
                Token = AccessToken,
                QueryParams = new QueryParams
                {
                    Expand = true
                },
                PageUrl = "General/Skeletons",
                Host = "core-dev10.secupay-ag.de"
            };

            Assert.IsTrue(request.GetPathAndQueryString().Contains("expand"));
            var list = RestService.GetList<Skeleton>(request);

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_5_GET()
        {
            string id;
            {
                // Get Id prerequesit for test
                var request = new RestRequest
                {
                    Token = AccessToken,
                    QueryParams = new QueryParams
                    {
                        Count = 2,
                        Fields = new List<string> {"id"}
                    },
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };

                var list = RestService.GetList<Skeleton>(request);

                id = list.List.First().Id;
                Assert.IsNotNull(id);
            }

            {
                // {{host}}General/Skeletons/skl_xxxxx?expand=true
                var request = new RestRequest
                {
                    Token = AccessToken,
                    Id = id,
                    QueryParams = new QueryParams
                    {
                        Expand = true
                    },
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };

                Assert.IsTrue(request.GetPathAndQueryString().Contains("expand"));
                var list = RestService.GetObject<Skeleton>(request);

                Assert.AreEqual(list.Id, id);
            }
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_6_POST()
        {
            // POST {{host}}General/Skeletons
            var id = "SKL_" + Guid.NewGuid();

            //POST
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de",
                    Object = new Skeleton {A = "value Test A", B = "value Test B", Id = id}
                };

                var obj = RestService.PostObject<Skeleton>(request);
                Assert.AreEqual(obj.Id, id);
            }
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_7_PUT()
        {
            // POST {{host}}General/Skeletons

            ObjectList<Skeleton> data;
            Skeleton obj;
            // {{host}}General/Skeletons?count=10&offset=5&fields=a,b&sort[a]=asc
            {
                var request = new RestRequest
                {
                    Method = WebRequestMethods.Http.Get,
                    Token = AccessToken,
                    QueryParams = new QueryParams
                    {
                        Count = 10,
                        Offset = 0,
                        SortOrder = new Dictionary<string, string>() {{"id", QueryParams.SortAsc}}
                    },
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };

                data = RestService.GetList<Skeleton>(request);
                obj = data.List.First();
            }

            obj.A = "TEST TEST TEST";

            //PUT
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de",
                    Object = obj,
                    Id = obj.Id
                };

                var objPut = RestService.PutObject<Skeleton>(request);

                Assert.AreEqual(objPut.A, "TEST TEST TEST");
            }

            //DELETE
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de",
                    Id = obj.Id
                };

                RestService.DeleteObject(request);
            }
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_General_Skeleton_8_EXECUTE()
        {
            {
                var request = new RestRequest
                {
                    Token = AccessToken,
                    Id = "12345",
                    Object = new Demoevent
                    {
                        Delay = 5,
                        Target = "xxx",
                        Type = "xxx",
                        Data = "{ whatever: \"whole object gets send as payload for event\"}"
                    },
                    ObjetType = typeof (Demoevent),
                    Action = "Demoevent",
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };

                var data = RestService.Execute<ResultClass>(request);
            }
        }
    }
}