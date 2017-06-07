namespace Secucard.Connect.Client
{
    /// <summary>
    /// Meta data describing the service.
    /// </summary>
    public class ServiceMetaData<T>
    {
        public readonly string Product;
        public readonly string Resource;
        public string AppId;
        public T ResourceType;

        public string ProductResource
        {
            get { return string.Format("{0}.{1}", Product, Resource); }
        }

        public ServiceMetaData(string product, string resource)
        {
            Product = product;
            Resource = resource;
        }
    }
}