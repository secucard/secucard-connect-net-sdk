namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ProductGroup
    {
        [DataMember(Name = "desc")]
        public string Desc { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        public override string ToString()
        {
            return "ProductGroup{" +
                   "id='" + Id + '\'' +
                   ", desc='" + Desc + '\'' +
                   ", level='" + Level + '\'' +
                   '}';
        }
    }
}