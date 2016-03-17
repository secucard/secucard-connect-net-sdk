/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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

        public static readonly ServiceMetaData<Checkin> META_DATA = new ServiceMetaData<Checkin>("smart",
            "checkins");

        protected override ServiceMetaData<Checkin> GetMetaData()
        {
             return META_DATA; 
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