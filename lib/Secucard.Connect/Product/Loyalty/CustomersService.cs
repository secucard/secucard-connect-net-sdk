namespace Secucard.Connect.Product.Loyalty
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Loyalty.Model;
    using System.Linq;
    using General.Model;

    public class CustomerLoyaltyService : ProductService<Customer>
    {
        public static readonly ServiceMetaData<Customer> MetaData = new ServiceMetaData<Customer>("loyalty",
            "customers");

        protected override ServiceMetaData<Customer> GetMetaData()
        {
            return MetaData;
        }

        public new ObjectList<Customer> GetList(QueryParams queryParams)
        {
            var list = base.GetList(queryParams);
            PostProcess(list.List);
            return list;
        }

        public new Customer Get(string customerId)
        {
            var store = base.Get(customerId);
            PostProcess(new List<Customer> {store});
            return store;
        }

        /// <summary>
        /// Post processing to retrieve image data
        /// </summary>
        private static void PostProcess(IEnumerable<Customer> list)
        {
            Parallel.ForEach(list, obj =>
            {
                // Actually there are 3 "Contact" attributes in "Customer" which contains picture object
                foreach (var pictureAttribute in obj.GetType().GetFields().Where(p => p.GetType() == typeof(Contact)))
                {
                    var mediaResource = (MediaResource)pictureAttribute.GetValue(obj);
                    if (mediaResource != null && !mediaResource.IsCached)
                    {
                        mediaResource.Download();
                    }
                }
            });
        }
    }
}