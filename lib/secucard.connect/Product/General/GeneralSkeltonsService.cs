namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Model.General;

    public class GeneralSkeletonsService: ProductService<Skeleton>
    {
        protected override ServiceMetaData<Skeleton> CreateMetaData()
        {
            return new ServiceMetaData<Skeleton>("general", "skeletons");
        }

        //public Skeleton GetSkeleton(string id)
        //{
        //    return GetChannel().GetObject<Skeleton>(id);
        //}

        //public ObjectList<Skeleton> GetSkeletons(QueryParams queryParams)
        //{
        //    return GetChannel().FindObjects<Skeleton>(queryParams);
        //}
    }
}
