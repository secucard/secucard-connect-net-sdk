namespace Secucard.Connect.Net.Util
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class JsonSerializer
    {
        public static T DeserializeJson<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                return (T) serializer.ReadObject(ms);
            }
        }

        public static List<T> DeserializeJsonList<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<T>));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                return (List<T>)serializer.ReadObject(ms);
            }
        }

        public static string SerializeJson<T>(T data)
        {
            if (data == null) return null;
            var serializer = new DataContractJsonSerializer(data.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        public static string SerializeJsonList<T>(List<T> data) 
        {
            if (data == null) return null;
            var serializer = new DataContractJsonSerializer(data.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}