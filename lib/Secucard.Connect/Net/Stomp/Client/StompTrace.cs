namespace Secucard.Connect.Net.Stomp.Client
{
    using System;
    using System.Diagnostics;
    using Secucard.Connect.Client;

    internal static class StompTrace
    {
        internal static void Info(Exception e)
        {
            Info("Exception: {0}", e.Message);
            if (e.InnerException != null) Info("Inner exception: {0}", e.InnerException.Message);
        }

        internal static void Info(string fmt, params object[] param)
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = string.Empty;
            if (type != null) name = type.Name;

            var source = string.Format("{0}.{1}", name, method.Name);
            SecucardTrace.InfoSource(source, fmt, param);
        }
    }
}