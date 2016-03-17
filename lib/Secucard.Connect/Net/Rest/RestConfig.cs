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

namespace Secucard.Connect.Net.Rest
{
    using Secucard.Connect.Client.Config;

    public class RestConfig
    {
        public RestConfig(Properties properties)
        {
            Url = properties.Get("Rest.Url");
            ResponseTimeoutSec = properties.Get("Rest.ResponseTimeoutSec", 300);
            ConnectTimeoutSec = properties.Get("Rest.ConnectTimeoutSec", 300);
        }

        public RestConfig()
        {
            ResponseTimeoutSec = 300;
            ConnectTimeoutSec = 300;
        }

        public string Url { get; set; }
        public int ResponseTimeoutSec { get; set; }
        public int ConnectTimeoutSec { get; set; }

        public override string ToString()
        {
            return "RestConfig [" + "Url = " + Url + "]";
        }
    }
}