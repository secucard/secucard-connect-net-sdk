namespace Secucard.Connect.Trace
{
    using System;
    using System.Collections.Concurrent;
    using System.Text;

    public class SecucardTraceMemory : ISecucardTrace
    {
        private ConcurrentQueue<string> MessageQueue;
        private int Limit;

        public SecucardTraceMemory()
        {
            MessageQueue = new ConcurrentQueue<string>();
            Limit = 1000;
        }

        public void Error(Exception e)
        {
            Info("Exception: {0}", e.Message);
            if (e.InnerException != null) Error("Inner exception: {0}", e.InnerException.Message);
        }

        public void Error(string fmt, params object[] param)
        {
            WriteToQueue("Error",fmt,param);
        }

        public void Info(string fmt, params object[] param)
        {
            WriteToQueue("Info", fmt, param);
        }

        private void WriteToQueue(string level, string fmt, params object[] param)
        {
            var msg = string.Format("{0:yyyyMMdd-HHmmss.fff}", DateTime.Now) + ":" + level + ":" + string.Format(fmt, param);
            MessageQueue.Enqueue(msg);

            // remove messages from queue if limit exceeded
            lock (this)
            {
                string overflow;
                while (MessageQueue.Count > Limit && MessageQueue.TryDequeue(out overflow))
                {}
            }
        }

        public string GetAllTrace()
        {
            var sb = new StringBuilder();
            while (!MessageQueue.IsEmpty)
            {
                string line;
                MessageQueue.TryDequeue(out line);
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

    }
}