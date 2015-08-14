namespace Secucard.Connect.Product.Smart
{
    using System.Collections.Generic;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Smart.Event;
    using Secucard.Connect.Product.Smart.Model;

    public class CheckinsService : ProductService<Checkin>
    {
        public CheckinEventHandler CheckinEvent;

        protected override ServiceMetaData<Checkin> CreateMetaData()
        {
            return new ServiceMetaData<Checkin>("smart", "checkins");
        }

        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, "smart.checkins", "changed", OnNewEvent);
        }

        private void OnNewEvent(object obj)
        {
            if (CheckinEvent != null)
                CheckinEvent(this, new CheckinEventEventArgs {SecucardEvent = (Event<Checkin>) obj});
        }

        /**
   * Returning check in data.<br/>
   * Data may contain a downloadable image. When the method returns either by callback or directly it is
   * guarantied that all images are completely downloaded to an local cache. To get the image content simply call
   * {@link MediaResource#getInputStream()}  or
   * {@link MediaResource#getContents()}. The streaming approach should be favored,
   * since the other obviously loads the content completely in memory before returning, which may not be optimal in any
   * case.<br/>
   * Note: Depending on the network it may of course take a while to download all, so without using an callback the
   * method may block until ready. Id that's not acceptable use the callback.
   *
   * @param callback Callback for async result processing.
   * @return If no callback is provided a list of check in data objects or null if no data could be found.
   * Returns always null if a callback was provided, the callbacks methods return the result analogous.
   */

        public List<Checkin> GetAll()
        {
            //Options options = getDefaultOptions();
            //options.resultProcessing = new Callback.Notify<ObjectList<Checkin>>() {
            //  @Override
            //  public void notify(ObjectList<Checkin> result) {
            //    processCheckins(result);
            //  }
            //};
            //return super.getSimpleList(null, options, callback);
            var objectList = GetList(new QueryParams());
            processCheckins(objectList);
            return objectList.List;
        }

        /**
   * Downloading check in pictures sequentially and return just when done.
   * todo: consider doing it in parallel to speed up, but not sure if rest channel can handle this properly
   */

        private void processCheckins(ObjectList<Checkin> checkins)
        {
            if (checkins != null && checkins.List != null)
            {
                foreach (var checkin in checkins.List)
                {
                    //MediaResource picture = checkin.GetPictureObject();
                    //if (picture != null) {
                    //  if (!picture.isCached()) {
                    //    picture.download();
                    //  }
                    //}
                }
            }
        }
    }
}

