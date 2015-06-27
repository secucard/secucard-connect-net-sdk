/*
 * * Configuration data of the client.
 */

using System;
using Secucard.Connect;
using Secucard.Connect.Auth;

namespace Secucard.Connect
{
    using Secucard.Stomp;

    public class ClientConfiguration
    {
        public RestConfig RestConfig { get; set; }
        public StompConfig StompConfig { get; set; }
        public string defaultChannel { get; set; }
        public int heartBeatSec { get; set; }
        public bool stompEnabled { get; set; }
        public string cacheDir { get; set; }
        public bool androidMode { get; set; }
        public int authWaitTimeoutSec { get; set; }
        public string oauthUrl { get; set; }
        public ClientCredentials clientCredentials { get; set; }
        public UserCredentials userCredentials { get; set; }
        public string deviceId { get; set; }
        public string authType { get; set; }

        //public ClientConfiguration(Properties properties) {
        //    try {
        //        init(properties);
        //    } catch (Exception e) {
        //        throw new SecuException("Error loading configuration", e);
        //    }
        //}

        //public void init(Properties cfg) 
        //{
        //            stompEnabled = Boolean.valueOf(cfg.getProperty("stompEnabled"));
        //    if (stompEnabled) {
        //        defaultChannel = cfg.getProperty("defaultChannel");
        //    } else
        //} {
        //      defaultChannel = ClientContext.REST;
        //    }

        //heartBeatSec = Integer.valueOf(cfg.getProperty("heartBeatSec"));
        //cacheDir = cfg.getProperty("cacheDir");
        //authWaitTimeoutSec = Integer.valueOf(cfg.getProperty("auth.waitTimeoutSec"));
        //oauthUrl = cfg.getProperty("auth.oauthUrl");
        //clientCredentials = new ClientCredentials(cfg.getProperty("auth.clientId"), cfg.getProperty("auth.clientSecret"));
        //deviceId = cfg.getProperty("device");
        //authType = cfg.getProperty("auth.type");
        //androidMode = Boolean.valueOf(cfg.getProperty("androidMode"));

        //stompConfiguration = new Configuration(
        //    cfg.getProperty("stomp.host"),
        //    cfg.getProperty("stomp.virtualHost"),
        //    Integer.valueOf(cfg.getProperty("stomp.port")),
        //    cfg.getProperty("stomp.destination"),
        //    cfg.getProperty("stomp.user"),
        //    cfg.getProperty("stomp.password"),
        //    Boolean.valueOf(cfg.getProperty("stomp.ssl")),
        //    cfg.getProperty("stomp.replyQueue"),
        //    Integer.valueOf(cfg.getProperty("stomp.connTimeoutSec")),
        //    Integer.valueOf(cfg.getProperty("stomp.messageTimeoutSec")),
        //    Integer.valueOf(cfg.getProperty("stomp.maxMessageAgeSec")),
        //    Integer.valueOf(cfg.getProperty("stomp.socketTimeoutSec")),
        //    1000 * heartBeatSec);

        //restConfiguration = new com.secucard.connect.channel.rest.Configuration(cfg.getProperty("rest.url"));
    }


}

  
  
