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

namespace Secucard.Connect.Net.Util
{
    using System;
    using System.Collections.Generic;

    public class JsonSplitter
    {
        private int currentPos;
        private string jsonString;

        public Dictionary<string, string> CreateDictionary(string JsonString)
        {
            currentPos = 0;
            jsonString = JsonString.Trim();

            var dict = new Dictionary<string, string>();

            while (currentPos < jsonString.Length)
            {
                var name = GetNextName();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var value = GetNextValue();
                    dict.Add(name, value);
                    currentPos++;
                }
                else
                {
                    currentPos = jsonString.Length;
                }
            }

            return dict;
        }

        private string GetNextName()
        {
            var start = jsonString.IndexOf("\"", currentPos, StringComparison.Ordinal) + 1;
            if (start > 0)
            {
                var end = jsonString.IndexOf("\"", start, StringComparison.Ordinal);

                if (end > 0)
                {
                    currentPos = end;
                    return jsonString.Substring(start, end - start);
                }
            }
            return null;
        }

        private string GetNextValue()
        {
            currentPos = jsonString.IndexOf(":", currentPos, StringComparison.Ordinal);

            var startValue = currentPos;
            var endValue = currentPos;
            var brackets = 0;

            // find start
            var startfound = false;
            while (!startfound)
            {
                currentPos++;
                var c = jsonString.Substring(currentPos, 1);

                // TODO: int und date

                //  string
                if (c == "\"")
                {
                    startfound = true;
                    currentPos++;
                    startValue = currentPos;
                    var endfound = false;
                    while (!endfound)
                    {
                        currentPos++;
                        var c2 = jsonString.Substring(currentPos, 1);
                        if (c2 == "\"")
                        {
                            endfound = true;
                            endValue = currentPos;
                        }
                    }
                }

                // object
                if (c == "{")
                {
                    brackets++;
                    startfound = true;
                    startValue = currentPos;
                    currentPos++;
                    var endfound = false;
                    while (!endfound)
                    {
                        currentPos++;
                        var c2 = jsonString.Substring(currentPos, 1);
                        if (c2 == "{")
                        {
                            brackets++;
                        }
                        if (c2 == "}")
                        {
                            brackets--;
                            if (brackets == 0)
                            {
                                currentPos++;
                                endfound = true;
                                endValue = currentPos;
                            }
                        }
                    }
                }
            }


            return jsonString.Substring(startValue, endValue - startValue).Trim();
        }
    }
}