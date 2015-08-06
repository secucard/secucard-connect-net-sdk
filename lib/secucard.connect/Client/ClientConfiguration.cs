namespace Secucard.Connect.Client
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using Secucard.Connect.auth;
    using Secucard.Connect.rest;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Stomp;

    public class ClientConfiguration
    {
        public string DefaultChannel { get; set; }
        public int? HeartBeatSec { get; set; }
        public bool StompEnabled { get; set; }
        public string CacheDir { get; set; }
        public bool AndroidMode { get; set; }
        public string DeviceId { get; set; }
        public string AppId { get; set; }

        [XmlIgnore]
        public RestConfig RestConfig { get; set; }
        [XmlIgnore]
        public StompConfig StompConfig { get; set; }
        [XmlIgnore]
        public AuthConfig AuthConfig { get; set; }

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
            var serializer = new XmlSerializer(typeof(ClientConfiguration));
            ClientConfiguration clientConfiguration;
            using (var streamReader = new StreamReader(filename))
            {
                clientConfiguration = (ClientConfiguration)serializer.Deserialize(streamReader);
                streamReader.Close();
            }
            return clientConfiguration;
        }

        public static ClientConfiguration GetDefault()
        {
            throw new NotImplementedException();
        }
    }
}

  
  
