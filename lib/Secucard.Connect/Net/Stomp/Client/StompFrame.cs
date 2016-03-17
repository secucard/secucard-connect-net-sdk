/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Net.Stomp.Client
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
        public int? TimeoutSec { get; set; }
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

            var frame = new StompFrame(line?.Trim() ?? string.Empty);

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