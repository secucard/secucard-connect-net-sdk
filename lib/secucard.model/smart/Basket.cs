namespace Secucard.Model.Smart
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Basket {

        [DataMember(Name = "products")]
        private List<Product> Products = new List<Product>();

        [DataMember(Name = "texts")]
        private List<Text> Texts = new List<Text>();

       
        //// Returns a mixed list of products followed by belonging texts.
        //public List<object> getProductsWithText() 
        //{
        //    var merged = new List<object>();

        //    foreach (Product product  in products) {
        //        var id = product.Id;
        //        merged.Add(product);
        //        merged.AddRange(texts.Where(text => text.ParentId == id.ToString()).Cast<object>());
        //    }
        //    return merged;
        //}

        
        public override string ToString() {
            return "Basket{" +
                   "products=" + Products +
                   ", texts=" + Texts +
                   '}';
        }
    }
}
