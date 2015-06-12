namespace Secucard.Stomp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class StompFrame
    {
        private const string EOL = "\n";

        public StompFrame(string command)
        {
            Headers = new Dictionary<string, string>();
            Command = command;
            CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; set; }
        public string Command { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return string.Format("Command:{0}, Headers:{1}, Body:{2}", Command, Headers.Count, Body.Length);
        }

        public string GetFrame()
        {
            var sb = new StringBuilder();

            // Command
            sb.Append(Command).Append(EOL);

            // Header
            foreach (var entry in Headers) sb.AppendFormat("{0}:{1}{2}", entry.Key, entry.Value, EOL);

            // Body after one LineFeed
            sb.Append(EOL);
            if (!string.IsNullOrWhiteSpace(Body)) sb.Append(Body);

            //sb.Append("\0");

            return sb.ToString();
        }

        public static StompFrame CreateFrame(byte[] bytes)
        {
            var msg = Encoding.UTF8.GetString(bytes).TrimStart();
            var reader = new StringReader(msg);

            var line = reader.ReadLine();

            var frame = new StompFrame(line.Trim());

            // read header
            var contentLength = 0;
            line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                var idx = line.IndexOf(':');
                if (idx > 0)
                {
                    var key = line.Substring(0, idx).ToLower();
                    if (!frame.Headers.ContainsKey(key))
                    {
                        var value = line.Substring(idx + 1);
                        frame.Headers.Add(key, value);
                        if (key == "content-length")
                        {
                            contentLength = Convert.ToInt32(value);
                        }
                    }
                }
                line = reader.ReadLine();
            }

            // read body
            if (contentLength > 0)
            {
                var buf = new char[contentLength];
                reader.Read(buf, 0, contentLength);
                frame.Body = new string(buf).Trim();
            }
            else
            {
                var sb = new StringBuilder();
                int b;
                while ((b = reader.Read()) != -1 && b != 0)
                {
                    sb.Append((char) b);
                }
                frame.Body = sb.ToString();
            }
            return frame;
        }
    }
}