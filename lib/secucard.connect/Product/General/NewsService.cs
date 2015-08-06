namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;

    public class NewsService : ProductService<News>
    {

        protected override ServiceMetaData<News> CreateMetaData()
        {
            return new ServiceMetaData<News>("general", "news");
        }
    }
}
