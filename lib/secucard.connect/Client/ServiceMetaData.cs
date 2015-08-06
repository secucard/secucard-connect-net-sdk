namespace Secucard.Connect.Client
{
    using Secucard.Connect.Product.Common.Model;

    /**
   * Meta data describing the service.
   */
    public class ServiceMetaData<T> where T : SecuObject
    {
        public string Product;
        public string Resource;
        public string AppId;
        public T ResourceType;


        public ServiceMetaData (string product, string resource)
        {
            Product = product;
            Resource = resource;
        }

        /**
         * Returns the object string for the service.
         */
        public string GetObject()
        {
            return Product + "." + Resource;
        }

        public string[] GetObjectArray()
        {
            return new string[] { Product, Resource };
        }
    }
}
