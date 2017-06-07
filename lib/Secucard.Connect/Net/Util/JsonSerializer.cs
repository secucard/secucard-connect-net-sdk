namespace Secucard.Connect.Net.Util
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web.Script.Serialization;

    public static class JsonSerializer
    {

        public static T TryDeserializeJson<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                try
                {
                    return (T)serializer.ReadObject(ms);
                }
                catch 
                {
                   return default(T);
                }
            }
        }

        public static T DeserializeJson<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            try
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
                {
                    return (T)serializer.ReadObject(ms);
                }
            }
            catch (System.Exception ex)
            {
                // Under some circumstances you can not deserialize demo result
                System.Diagnostics.Debug.WriteLine($"Error message { ex.Message }");

                return default(T);
            }
        }

        public static List<T> DeserializeJsonList<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof (List<T>));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                return (List<T>) serializer.ReadObject(ms);
            }
        }

        public static string SerializeJson<T>(T data)
        {
            if (data == null) return null;
            var serializer = new DataContractJsonSerializer(data.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.Unicode.GetString(ms.ToArray());
            }
        }

        public static string SerializeJsonList<T>(List<T> data)
        {
            if (data == null) return null;
            var serializer = new DataContractJsonSerializer(data.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.Unicode.GetString(ms.ToArray());
            }
        }

        public static Dictionary<string, object> DeserializeToDictionary(string jsonResult)
        {
            var js = new JavaScriptSerializer();
            var result = js.Deserialize<Dictionary<string, object>>(jsonResult);

            return result;
        }
    }
}