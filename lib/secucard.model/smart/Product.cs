namespace Secucard.Model.Smart
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Product
    {

        [DataMember(Name = "id")]
        public int Id;

        [DataMember(Name = "parent")]
        public int Parent;

        [DataMember(Name = "articleNumber")]
        public string ArticleNumber;

        [DataMember(Name = "ean")]
        public string Ean;

        [DataMember(Name = "desc")]
        public string Desc;

        [DataMember(Name = "quantity")]
        public decimal Quantity;

        [DataMember(Name = "priceOne")]
        public int PriceOne;

        [DataMember(Name = "tax")]
        public int Tax;

        [DataMember(Name = "group")]
        public List<ProductGroup> Groups = new List<ProductGroup>();

        //public Product() {
        //}

        //public Product(int id, int parent, string articleNumber, string ean, string desc, string quantity, int priceOne,
        //    int tax, List<ProductGroup> productGroups) {
        //    this(id, parent, articleNumber, ean, desc, new decimal(Convert.ToDouble(quantity)), priceOne, tax, productGroups);
        //    }

        //public Product(int id, int parent, string articleNumber, string ean, string desc, decimal quantity,
        //    int priceOne, int tax, List<ProductGroup> groups) {
        //    this.id = id;
        //    this.parent = parent;
        //    this.articleNumber = articleNumber;
        //    this.ean = ean;
        //    this.desc = desc;
        //    this.quantity = quantity;
        //    this.priceOne = priceOne;
        //    this.tax = tax;
        //    this.groups = groups;
        //    }



        //@JsonIgnore
        //public void addProductGroup(ProductGroup group) {
        //    groups.add(group);
        //}



        public override string ToString()
        {
            return "Product{" +
                   "id='" + Id + '\'' +
                   ", parent='" + Parent + '\'' +
                   ", articleNumber='" + ArticleNumber + '\'' +
                   ", ean='" + Ean + '\'' +
                   ", desc='" + Desc + '\'' +
                   ", quantity=" + Quantity +
                   ", priceOne=" + PriceOne +
                   ", tax=" + Tax +
                   ", productGroups=" + Groups +
                   '}';
        }
    }
}
