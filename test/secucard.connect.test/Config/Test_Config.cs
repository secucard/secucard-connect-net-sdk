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

namespace Secucard.Connect.Test.Config
{
    using System.Diagnostics;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Client.Config;

    [TestClass]
    public class Test_Config
    {
        [TestMethod]
        [TestCategory("Config")]
        [DeploymentItem("Data//Config", "Data//Config")]
        public void Test_Config_Properties()
        {
            const string configPath = "Data//Config//SecucardConnect.config";
            if (File.Exists(configPath)) File.Delete(configPath);

            Properties props = new Properties();
            props.Set("Appid", "ApplikationId");
            props.Set("test", "test");
            props.Save(configPath);

            var props2 = Properties.Load(configPath);
            Debug.Write(File.ReadAllText(configPath));

            Assert.AreEqual(props.Get("Appid"), props2.Get("Appid"));
        }
    }
}