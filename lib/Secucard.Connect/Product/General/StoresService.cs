namespace Secucard.Connect.Product.General
{
    using System.Collections.Generic;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    public class StoresService : ProductService<Store>
    {
        public static readonly ServiceMetaData<Store> MetaData = new ServiceMetaData<Store>("general", "stores");

        protected override ServiceMetaData<Store> GetMetaData()
        {
            return MetaData;
        }

        /// <summary>
        /// Check in the store with the given id.
        /// </summary>
        public bool CheckIn(string storeId)
        {
            return ExecuteToBool(storeId, "checkin", null, null, null);
        }

        /// <summary>
        /// Check out of the store with the given id.
        /// </summary>
        public bool CheckOut(string storeId)
        {
            return ExecuteToBool(storeId, "checkin", "false", null, null);
        }

        /// <summary>
        /// Set store with given id as default.
        /// </summary>
        public bool SetDefault(string storeId)
        {
            return ExecuteToBool(storeId, "setDefault", null, null, null);
        }

        public new ObjectList<Store> GetList(QueryParams queryParams)
        {
            var list = base.GetList(queryParams);
            ProcessStore(list.List);
            return list;
        }

        public new Store Get(string storeId)
        {
            var store = base.Get(storeId);
            ProcessStore(new List<Store> {store});
            return store;
        }

        /// <summary>
        /// Post processing to retrieve image data
        /// </summary>
        private static void ProcessStore(List<Store> stores)
        {
            foreach (var obj in stores)
            {
                var mediaResource = obj.Logo;
                if (mediaResource != null)
                {
                    if (!mediaResource.IsCached)
                    {
                        mediaResource.Download();
                    }
                }
            }
        }
    }
}