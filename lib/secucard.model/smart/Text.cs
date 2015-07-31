namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Text
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "desc")]
        public string Desc { get; set; }

        [DataMember(Name = "parent", EmitDefaultValue = false)]
        public int? ParentId { get; set; }

        public override string ToString()
        {
            return "Text{" +
                   "parentId='" + ParentId + '\'' +
                   ", desc='" + Desc + '\'' +
                   '}';
        }
    }
}