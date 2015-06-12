namespace Secucard.Stomp
{
    using System;
    using System.Diagnostics;

    internal class StompTrace
    {
        internal static void ClientTrace(Exception e)
        {
            ClientTrace("Exception: {0}", e.Message);
            if (e.InnerException != null) ClientTrace("Inner exception: {0}", e.InnerException.Message);
        }

        internal static void ClientTrace(string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format("{0:yyyy-MM-dd-HHmmss}: ", DateTime.Now) + string.Format(fmt, param));
        }
    }
}