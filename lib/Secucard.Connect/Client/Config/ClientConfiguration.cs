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

namespace Secucard.Connect.Client.Config
{
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp;
    using Secucard.Connect.Storage;

    public class ClientConfiguration
    {
        public IClientAuthDetails ClientAuthDetails { get; set; }
        public DataStorage DataStorage { get; set; }
        public Properties Properties { get; set; }

        internal string DefaultChannel { get; set; }
        internal bool StompEnabled { get; set; }
        internal string AppId { get; set; }
        internal string TraceDir { get; set; }
        internal string CacheDir { get; set; }
        internal RestConfig RestConfig { get; set; }
        internal StompConfig StompConfig { get; set; }
        internal AuthConfig AuthConfig { get; set; }

        public override string ToString()
        {
            return "ClientConfiguration [ " +
                   "DefaultChannel = " + DefaultChannel + "," +
                   "StompEnabled = " + StompEnabled + "," +
                   "AppId = " + AppId + "," +
                   "TraceDir = " + TraceDir + "," +
                   "CacheDir = " + CacheDir + "," +
                   "RestConfig = " + RestConfig + "," +
                   "StompConfig = " + StompConfig + "," +
                   "AuthConfig = " + AuthConfig + "]";
        }

        public void Save(string filename)
        {
            var serializer = new XmlSerializer(typeof (ClientConfiguration));
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, this);
            textWriter.Close();
        }

        public static ClientConfiguration Load(string filename)
        {
            var fileStream = new FileStream(filename, FileMode.Open);
            return Load(fileStream);
        }

        public static ClientConfiguration GetDefault()
        {
            var stream =
                Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Secucard.Connect.Client.Config.SecucardConnect.config");
            return Load(stream);
        }

        #region ### Ctor ###

        private static ClientConfiguration Load(Stream stream)
        {
            return new ClientConfiguration(Properties.Load(stream));
        }

        public ClientConfiguration(Properties properties)
        {
            Properties = properties;
            DefaultChannel = properties.Get("DefaultChannel", "rest");
            StompEnabled = properties.Get("Stomp.Enabled", false);
            AppId = properties.Get("AppId");
            TraceDir = properties.Get("TraceDir");
            CacheDir = properties.Get("CacheDir");

            RestConfig = new RestConfig(properties);
            AuthConfig = new AuthConfig(properties);
            StompConfig = new StompConfig(properties);
        }

        #endregion
    }
}