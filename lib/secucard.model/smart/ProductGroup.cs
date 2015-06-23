namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ProductGroup
    {

        [DataMember(Name = "id")]
        public string Id;

        [DataMember(Name = "desc")]
        public string Desc;

        [DataMember(Name = "level")]
        public int Level;

        //public ProductGroup() {
        //}

        //public ProductGroup(string id, string desc, int level) {
        //    this.id = id;
        //    this.desc = desc;
        //    this.level = level;
        //}


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
