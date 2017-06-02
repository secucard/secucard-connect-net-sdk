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