namespace Secucard.Connect.Trace
{
    using System;
    using System.Diagnostics;

    public class SecucardTraceFile : ISecucardTrace
    {
        public SecucardTraceFile(string fullFilePath)
        {
            var listener = new SecucardTraceListener(fullFilePath);
            Trace.Listeners.Add(listener);
        }

        public void Error(Exception e)
        {
            Info("Exception: {0}", e.Message);
            if (e.InnerException != null) Error("Inner exception: {0}", e.InnerException.Message);
        }

        public void Error(string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format(fmt, param), "Error");
        }

        public void Info(string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format(fmt, param), "Info");
        }
    }
}