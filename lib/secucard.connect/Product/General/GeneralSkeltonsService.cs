namespace Secucard.Connect.Product.General
{
    using Secucard.Model.General;

    public class GeneralSkeltonsService: ServiceBase
    {
        public Skeleton GetSkeleton(string id)
        {
            return RestChannel.GetObject<Skeleton>(id);


            //            return new Invoker<Skeleton>() {
            //  @Override
            //  protected Skeleton handle(Callback<Skeleton> callback) {
            //    return getRestChannel().getObject(Skeleton.class, id, callback);
            //  }
            //}.invoke(callback);

            //}

            //    public List<Skeleton> getSkeletons(final QueryParams queryParams, final Callback<List<Skeleton>> callback) {
            //return new ConvertingInvoker<ObjectList<Skeleton>, List<Skeleton>>() {
            //  @Override
            //  protected ObjectList<Skeleton> handle(Callback<ObjectList<Skeleton>> callback) {
            //    return getRestChannel().findObjects(Skeleton.class, queryParams, callback);
            //  }

            //  @Override
            //  protected List<Skeleton> convert(ObjectList<Skeleton> object) {
            //    return object == null ? null : object.getList();
            //  }
            //}.invokeAndConvert(callback);
            //}
        }
    }
}
