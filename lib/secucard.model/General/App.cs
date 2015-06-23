namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class App : SecuObject
    {
        public override string SecuObjectName
        {
            get { return "general.apps"; }
        }

        [DataMember(Name = "name")] public string Name;
    }
}