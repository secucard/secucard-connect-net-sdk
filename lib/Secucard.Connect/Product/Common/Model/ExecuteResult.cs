namespace Secucard.Connect.Product.Common.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Generic result container used as payload (data) 
    /// </summary>
    [DataContract]
    public class ExecuteResult
    {
        [DataMember(Name = "result")]
        public string Result { get; set; }

        [DataMember(Name = "request")]
        public string Request { get; set; }
    }
}