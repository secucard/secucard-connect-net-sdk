using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secucard.Connect
{
    using Secucard.Connect.Auth;
    using Secucard.Connect.Product;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public event SecucardConnectEvent SecucardConnectEvent;

        #region ### Start / Stop ###

        public void Connect()
        {
            // Start Authentification
        }

        public void CancelAuth()
        {
        }

        #endregion

        #region ### Factory Client ### 

        public static SecucardConnect Create(string id, AuthConfig config)
        {
            // Factory

            return new SecucardConnect();
        }

        #endregion

        #region ### Factory Service ###

        public T Getservice<T>() where T : ServiceBase
        {
            return null;
        }

        #endregion

    }


}
