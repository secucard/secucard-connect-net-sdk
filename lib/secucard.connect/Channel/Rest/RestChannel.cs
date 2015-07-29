namespace Secucard.Connect.Channel.Rest
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Rest;
    using Secucard.Model;

    public class RestChannel : AbstractChannel, IChannel
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

            RestService = new RestService(RestConfig);
        }


        #region ## IChannel ###

        public T GetObject<T>(string id) where T : SecuObject
        {
            var token = AuthProvider.GetToken(true);
            var request = new RestRequest
            {
                Token = token.AccessToken,
                Id = id,
                PageUrl = "General/Skeletons", // TODO: Resolver
                Host = "core-dev10.secupay-ag.de" // TODO: Config
            };

            var obj = RestService.GetObject<T>(request);

            return obj;
        }

        public ObjectList<T> FindObjects<T>(QueryParams query) where T : SecuObject
        {
            var token = AuthProvider.GetToken(true);
            var request = new RestRequest
            {
                Token = token.AccessToken,
                QueyParams = query,
                PageUrl = "General/Skeletons", // TODO: Resolver
                Host = "core-dev10.secupay-ag.de" // TODO: Config
            };

            var list = RestService.GetList<T>(request);

            return list;
        }

        #endregion

    }
}
