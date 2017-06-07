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

        public static readonly ServiceMetaData<Checkin> MetaData = new ServiceMetaData<Checkin>("smart",
            "checkins");

        protected override ServiceMetaData<Checkin> GetMetaData()
        {
             return MetaData; 
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

        public List<Checkin> GetAll()
        {
            var objectList = GetList(new QueryParams());
            ProcessCheckins(objectList.List);
            return objectList.List;
        }

        private static void ProcessCheckins(List<Checkin> checkins)
        {
            foreach (var checkin in checkins)
            {
                MediaResource picture = checkin.PictureObject;
                if (picture != null)
                {
                    if (!picture.IsCached)
                    {
                        picture.Download();
                    }
                }
            }
        }
    }
}