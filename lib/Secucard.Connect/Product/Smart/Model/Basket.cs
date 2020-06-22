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
        }

        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }

        public override string ToString()
        {
            return "Basket{" +
                   "products=" + Products +
                   '}';
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}