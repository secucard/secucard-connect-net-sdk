namespace Secucard.Connect.Channel.Rest
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Model;

    public class RestChannel : AbstractChannel
    {
        private RestConfig RestConfig;
        private string ChannelId;
        private RestService RestService;
        private AuthProvider AuthProvider;

        public RestChannel(RestConfig restConfig, string channelId, AuthProvider authProvider)
        {
            RestConfig = restConfig;
            ChannelId = channelId;// TODO: Needed?
            AuthProvider = authProvider;

            RestService = new RestService(new RestConfig { BaseUrl = restConfig.BaseUrl });
        }

        public T GetObject<T>(string id) where T : SecuObject
        {
            var token = AuthProvider.GetToken(true);
            var request = new RestRequest
            {
                Token = token.AccessToken,
                Id = id,
                PageUrl = "General/Skeletons",
                Host = "core-dev10.secupay-ag.de"
            };

            var obj = RestService.GetObject<T>(request);

            return obj;
        }



    }
}
