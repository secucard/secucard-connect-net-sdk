namespace Secucard.Connect.Client.Config
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class Properties
    {
        public Properties()
        {
            Elements = new List<Property>();
        }

        public List<Property> Elements { get; set; }

        public string this[string name]
        {
            get
            {
                return Get(name);
            }
            set
            {
                Set(name, value);
            }
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
            if(Elements.All(o => o.Name != name))
                Elements.Add(new Property {Name = name,Value = value});
            else
            {
                Elements.First(o => o.Name == name).Value = value;
            }
        }

        #region ### Methods ###

        public static Properties Read(string fullFilePath)
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

        internal static Properties Read(Stream stream)
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

        public void Write(string fullFilePath)
        {
            var xmlDoc = new XDocument(new XElement("Properties",
                from ele in Elements
                select new XElement("Property", new XAttribute("Name", ele.Name), ele.Value)));

            xmlDoc.Save(fullFilePath);
        }

        #endregion
    }
}