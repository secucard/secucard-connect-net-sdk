namespace Secucard.Connect.Client
{
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp.Client;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;

    public class ClientConfiguration
    {
        public Properties Properties;

        public string DefaultChannel { get; set; }
        internal bool StompEnabled { get; set; }
        public string AppId { get; set; }
        internal string TraceDir;

        public string CacheDir { get; set; }

        internal RestConfig RestConfig { get; set; }
        internal StompConfig StompConfig { get; set; }
        internal AuthConfig AuthConfig { get; set; }

        public ISecucardTrace SecucardTrace;
        public DataStorage DataStorage;
        public IClientAuthDetails ClientAuthDetails;

        public void Save(string filename)
        {
            var serializer = new XmlSerializer(typeof(ClientConfiguration));
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, this);
            textWriter.Close();
        }

        public static ClientConfiguration Load(string filename)
        {
            var fileStream = new FileStream(filename,FileMode.Open);
            return Load(fileStream);
        }

        public static ClientConfiguration Get()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Secucard.Connect.Client.Config.SecucardConnect.config");
            return Load(stream);
        }


        #region ### Ctor ###

        private static ClientConfiguration Load(Stream stream)
        {
            return new ClientConfiguration(Properties.Read(stream));
        }

        public ClientConfiguration(Properties properties)
        {
            Properties = properties;
            DefaultChannel = properties.Get("DefaultChannel", "rest");
            StompEnabled = properties.Get("StompEnabled", true);
            AppId = properties.Get("AppId");
            TraceDir= properties.Get("TraceDir");

            //TODO: Logging and Cache

            RestConfig = new RestConfig(properties);
            AuthConfig = new AuthConfig(properties);
            StompConfig = new StompConfig(properties);
        }

        #endregion

    }
}

  
  
