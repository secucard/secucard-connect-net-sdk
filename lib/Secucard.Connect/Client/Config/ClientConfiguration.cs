using System;

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
            if (stream == null) throw new ArgumentNullException(nameof(stream));
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