namespace secucard.connect.test.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Rest;
    using Secucard.Model;
    using Secucard.Model.General;

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
                Token = Token.AccessToken,
                QueyParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0,
                    SortOrder = new NameValueCollection { { "a", QueryParams.SORT_ASC } },
                    Fields = new List<string> { "a", "b" }
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
                    Token = Token.AccessToken,
                    QueyParams = new QueryParams
                    {
                        Count = 10,
                        ScrollExpire = "5m",
                        SortOrder = new NameValueCollection { { "a", QueryParams.SORT_ASC } },
                        Fields = new List<string> { "a", "b" }
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
                    Token = Token.AccessToken,
                    QueyParams = new QueryParams
                    {
                        ScrollId = scrollId,
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
                Token = Token.AccessToken,
                QueyParams = new QueryParams
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
                Token = Token.AccessToken,
                QueyParams = new QueryParams
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
                    Token = Token.AccessToken,
                    QueyParams = new QueryParams
                    {
                        Count = 2,
                        Fields = new List<string> { "id"}
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
                    Token = Token.AccessToken,
                    Id = id,
                        QueyParams = new QueryParams
                        {
                            Expand = true
                        },
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de"
                };

                Assert.IsTrue(request.GetPathAndQueryString().Contains("expand"));
                var list = RestService.GetObject<Skeleton>(request);

                Assert.AreEqual(list.Id,id);
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
                    Token = Token.AccessToken,
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de",
                    Object = new Skeleton { A = "value Test A", B = "value Test B", Id = id }
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
                    Token = Token.AccessToken,
                    QueyParams = new QueryParams
                    {
                        Count = 10,
                        Offset = 0,
                        SortOrder = new NameValueCollection { { "id", QueryParams.SORT_ASC } }
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
                    Token = Token.AccessToken,
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de",
                    Object = obj
                };

                var objPut = RestService.PutObject<Skeleton>(request);

                Assert.AreEqual(objPut.A, "TEST TEST TEST");
            }

            //DELETE
            {
                var request = new RestRequest
                {
                    Token = Token.AccessToken,
                    PageUrl = "General/Skeletons",
                    Host = "core-dev10.secupay-ag.de",
                    Id = obj.Id
                };

                var objPut = RestService.DeleteObject<Skeleton>(request);

            }
        }

    }
}