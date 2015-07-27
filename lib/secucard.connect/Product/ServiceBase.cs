using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secucard.Connect.Product
{
    using Secucard.Connect.Channel.Rest;

    public abstract class ServiceBase
    {
        public RestChannel RestChannel { get; set; }

    }
}
