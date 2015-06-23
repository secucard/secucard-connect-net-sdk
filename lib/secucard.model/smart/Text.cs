namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Text
    {

        [DataMember(Name = "parentId")]
        public string ParentId;

        [DataMember(Name = "desc")]
        public string Desc;

        //public Text() {
        //}

        //public Text(String parentId, String desc) {
        //    this.parentId = parentId;
        //    this.desc = desc;
        //}

        public override string ToString()
        {
            return "Text{" +
                   "parentId='" + ParentId + '\'' +
                   ", desc='" + Desc + '\'' +
                   '}';
        }
    }
}
