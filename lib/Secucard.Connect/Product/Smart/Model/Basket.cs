namespace Secucard.Connect.Product.Smart.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
            Texts = new List<Text>();
        }

        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }

        [DataMember(Name = "texts")]
        public List<Text> Texts { get; set; }

        public override string ToString()
        {
            return "Basket{" +
                   "products=" + Products +
                   ", texts=" + Texts +
                   '}';
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void AddProduct(Text text)
        {
            Texts.Add(text);
        }
    }
}