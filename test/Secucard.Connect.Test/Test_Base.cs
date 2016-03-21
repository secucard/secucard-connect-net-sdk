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

namespace Secucard.Connect.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Base
    {
        protected readonly ClientConfiguration Config;
        protected readonly StompConfig StompConfig;
        protected readonly RestConfig RestConfig;
        protected AuthConfig AuthConfig;

        protected const string configPath = "data\\Config\\SecucardConnect.config";
        protected readonly Properties properties;

        protected bool IsConnected;
        protected bool MessageReceived;

        protected string AccessToken;

        protected Test_Base()
        {
            properties = Properties.Load(configPath);

            // Setting current Access Token
            AccessToken = "i0tdt3fgv31vde64kp49db3827";

            Config = new ClientConfiguration(properties);
            StompConfig = new StompConfig(properties);
            RestConfig = new RestConfig(properties);
            AuthConfig = new AuthConfig(properties);
        }
    }
}