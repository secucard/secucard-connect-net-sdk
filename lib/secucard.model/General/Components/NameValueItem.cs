namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class NameValueItem
    {
        [DataMember(Name = "name")] public string Name;
        [DataMember(Name = "value")] public string Value;

        public override string ToString()
        {
            return string.Format("{0}:{1}",Name,Value);
        }
    }
}