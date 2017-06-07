namespace Secucard.Connect.Product.General
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    public class NewsService : ProductService<News>
    {
        public static readonly ServiceMetaData<News> MetaData = new ServiceMetaData<News>("general", "news");

        protected override ServiceMetaData<News> GetMetaData()
        {
            return MetaData;
        }

        /// <summary>
        ///  Mark news with given id as read.  
        ///  return True if successfully updated, false else
        /// </summary>
        public bool MarkRead(string id)
        {
            return ExecuteToBool(id, "markRead", null, null, null);
        }

        public new ObjectList<News> GetList(QueryParams queryParams)
        {
            var list = base.GetList(queryParams);
            PostProcess(list.List);
            return list;
        }

        public new News Get(string storeId)
        {
            var news = base.Get(storeId);
            PostProcess(new List<News> {news});
            return news;
        }

        /// <summary>
        /// Post processing to retrieve image data
        /// </summary>
        private static void PostProcess(IEnumerable<News> list)
        {
            Parallel.ForEach(list, obj =>
            {
                var mediaResource = obj.PictureObject;
                if (mediaResource != null)
                {
                    if (!mediaResource.IsCached)
                    {
                        mediaResource.Download();
                    }
                }
            });
        }
    }
}