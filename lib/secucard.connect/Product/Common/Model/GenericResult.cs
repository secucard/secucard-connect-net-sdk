namespace Secucard.Connect.Product.Common.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Payment.Model;

    public class GenericResult
    {
        public Dictionary<string, string> NameValuesLevelOne { get; set; }


        public GenericResult(string json)
        {
            // On return data contains an unknown object that will be treated as a string at first.
            // Workaround: MS json serializer does not have the option to convert object to string
            Dictionary<string, string> dict = new JsonSplitter().CreateDictionary(json);
        }

       
    }
}
