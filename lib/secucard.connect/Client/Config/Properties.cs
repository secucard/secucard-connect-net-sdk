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

namespace Secucard.Connect.Client.Config
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    public class Properties
    {
        public Properties()
        {
            Elements = new List<Property>();
        }

        private List<Property> Elements { get; set; }

        public string this[string name]
        {
            get { return Get(name); }
            set { Set(name, value); }
        }

        public bool Get(string name, bool defaultValue)
        {
            var value = Get(name, null);
            return value != null ? bool.Parse(value) : defaultValue;
        }

        public int Get(string name, int defaultValue)
        {
            var value = Get(name, null);
            return value != null ? int.Parse(value) : defaultValue;
        }

        public string Get(string name, string defaultValue = null)
        {
            var ele = Elements.FirstOrDefault(o => o.Name == name);

            return ele != null ? ele.Value : defaultValue;
        }

        public void Set(string name, string value)
        {
            if (Elements.All(o => o.Name != name))
                Elements.Add(new Property {Name = name, Value = value});
            else
            {
                Elements.First(o => o.Name == name).Value = value;
            }
        }

        #region ### Methods ###

        public static Properties GetDefault()
        {
            var stream =
                Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Secucard.Connect.Client.Config.SecucardConnect.config");
            return Load(stream);
        }

        public static Properties Load(string fullFilePath)
        {
            if (!File.Exists(fullFilePath)) throw new FileNotFoundException(fullFilePath);

            var xmlDoc = XDocument.Load(fullFilePath);

            var properties = new Properties
            {
                Elements = xmlDoc.Descendants("Property").ToList().Select(o => new Property
                {
                    Name = o.Attribute("Name").Value,
                    Value = o.Value
                }).ToList()
            };

            return properties;
        }

        internal static Properties Load(Stream stream)
        {
            var xmlDoc = XDocument.Load(stream);

            var properties = new Properties
            {
                Elements = xmlDoc.Descendants("Property").ToList().Select(o => new Property
                {
                    Name = o.Attribute("Name").Value,
                    Value = o.Value
                }).ToList()
            };

            return properties;
        }

        public void Save(string fullFilePath)
        {
            var xmlDoc = new XDocument(new XElement("Properties",
                from ele in Elements
                select new XElement("Property", new XAttribute("Name", ele.Name), ele.Value)));

            xmlDoc.Save(fullFilePath);
        }

        #endregion
    }
}