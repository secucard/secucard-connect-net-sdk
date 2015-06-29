using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secucard.Connect.Channel.Rest
{
    using Secucard.Connect.Rest;


    public class RestChannel : AbstractChannel
    {
        private RestConfig RestConfig;
        private string Id;

        public RestChannel(RestConfig restConfig, string id)
        {
            RestConfig = restConfig;
            Id = id;
        }


        public void Get(RestRequest request)
        {
            RestBase rest = new RestService(RestConfig);

        }


    }
}
